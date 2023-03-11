using System;
using System.Text;
using System.Windows.Forms; // MessageBox
using System.Text.RegularExpressions; // 正则表达式
using System.Windows.Input;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Data;
using System.Windows.Markup;
using static Nice.TransformFunc;

namespace Nice
{
    internal class TransformFunc
    {
        /***************************************************************/
        File g_f = new File();
        string g_dirPath = null;
        string g_fileName = null;
        string g_signBypass = @"CONFIGDATA[].WORDS.";
        //string g_signVio4 = @"REG=";
        /***************************************************************/

        enum cfg_type {
            VIO4,
            BYPASS,
            MAX_TYPE
        };

        public struct CFG {
            public string slaveId;
            public string reg;
            public string data;
            public string dataLen;
            public string delay;
        };

        public void TransformFuncEntry(string srcFilePath)
        {
            if (!CheckPath(srcFilePath)) {
                g_f.Log("[TransformFuncEntry] check path err");
                return;
            }
            if (!TransformStart()) {
                g_f.Log("[TransformFuncEntry] transform err");
                return;
            }
        }

        private bool CheckPath(string srcFilePath)
        {
            if (srcFilePath == null || srcFilePath == "")
            {
                g_f.Log("[CheckPath] err:文件路径检测fail");
                MessageBox.Show("err:文件路径检测fail", "CheckPath");
                return false;
            }
            int totalSize = Encoding.Default.GetByteCount("srcFilePath");
            int fileNameIndex = srcFilePath.LastIndexOf(@"\") + 1;
            if (fileNameIndex <= 0) {
                g_dirPath = g_f.g_currentPath + @"\";
                g_fileName = srcFilePath;
                return true;
            } else {
                g_dirPath = g_f.CutStr(srcFilePath, 1, fileNameIndex);
                g_fileName = System.IO.Path.GetFileName(srcFilePath);
                return true;
            }
        }

        private void TransformToVio4()
        {
            String format = " yyyy-MM-dd_hhmmssffff";
            DateTime date = DateTime.Now;
            string time = date.ToString(format, DateTimeFormatInfo.InvariantInfo);
            string filePath = g_dirPath + System.IO.Path.GetFileNameWithoutExtension(g_fileName) + time + ".ini";
            if (!System.IO.File.Exists(filePath)) {
                System.IO.File.Copy(g_dirPath + g_fileName, filePath, true);
            } else {
                //g_f.Log("err:请删除冗余同名文件" + filePath + @"[TransformToVio4]");
                System.IO.File.Copy(g_dirPath + g_fileName, filePath, true);
            }

            int totalLineNum = g_f.getMaxLine(g_dirPath + g_fileName);
            g_f.Clear(filePath);

            g_f.Log("totalLineNum=" + totalLineNum.ToString());
            string delayValue = null;
            bool dataDoneFlag = false;
            for (int i = 1; i <= totalLineNum; i++) {
                string currLineStr = g_f.getLine(g_dirPath + g_fileName, i).ToUpper();
                currLineStr = currLineStr.Replace(" ", ""); // move Space
                currLineStr = currLineStr.Replace("\t", ""); // move Tab
                /* 1.---- handle the comment // ---------------------- */
                if(currLineStr.Contains(@"//")) {
                    int beginPosition = currLineStr.IndexOf(@"/") + 1;
                    int endPosition = currLineStr.IndexOf(@"/", beginPosition) + 1;
                    if(beginPosition + 1 == endPosition) {
                        currLineStr = g_f.CutStr(currLineStr, 1, beginPosition);
                    } else {
                        currLineStr = g_f.CutStr(currLineStr, 1, beginPosition);
                    }
                    if (g_f.CutStr(currLineStr, 1, 2) == @"//") {
                        continue;
                    } else {
                        
                    }
                }
                /* 2.---- handle the comment /* --------------------- */
                if (currLineStr.Contains(@"/*")) {
                    int beginPosition = currLineStr.IndexOf(@"/");
                    int endPosition = currLineStr.IndexOf(@"*");
                    if(currLineStr.Contains(@"*/")) {
                        if(beginPosition + 1 == endPosition) {
                            currLineStr = g_f.CutStr(currLineStr, 1, beginPosition);
                        } else {
                            currLineStr = g_f.CutStr(currLineStr, 1, beginPosition);
                        }
                    }

                }
                /* 3.---- handle no the comment --------------------- */
                {
                    int indexSize = currLineStr.IndexOf(@"]") - currLineStr.IndexOf(@"[") - 1;
                    int equalSignPosition = currLineStr.IndexOf('=') + 1;
                    int semicolonSignPosition = currLineStr.IndexOf(';') + 1;
                    int cfgNameSize = equalSignPosition - g_signBypass.Length - indexSize - 1;
                    int cfgDataSize = semicolonSignPosition  - equalSignPosition - 1;
                    g_f.Log("第" + i.ToString() + "行:\t" + @"';'=" + semicolonSignPosition.ToString() + "'='=" + equalSignPosition + "\tcfgNameSize=" + cfgNameSize + "\tcfgDataSize=" + cfgDataSize + "\t" + currLineStr);
                    if (equalSignPosition <= 0 || semicolonSignPosition <= 0 || equalSignPosition >= semicolonSignPosition ||
                        semicolonSignPosition != (g_signBypass.Length + indexSize + cfgNameSize + cfgDataSize + 2)) { // 2: 1个'='  + 1个';'
                        //g_f.Log("[TransformToVio4] 不符合要求,行号：" + i.ToString());
                        continue;
                    }
                    string cfgNameStr = g_f.CutStr(currLineStr, g_signBypass.Length + indexSize + 1, cfgNameSize); // 截取name
                    string cfgDataStr = g_f.CutStr(currLineStr, equalSignPosition + 1, cfgDataSize); // 截取data
                    //g_f.Log("indexSize=" + indexSize.ToString() + @"  equalSign=" + equalSignPosition.ToString() + "\t type=" + cfgNameStr.ToLower());
                    //g_f.Log("+++ cfgNameStr=" + cfgNameStr + " cfgDataStr=" + cfgDataStr);
                    CFG cfg = new CFG() { slaveId = null, reg = null, data = null, dataLen = null, delay = null };
                    switch (cfgNameStr) {
                        case "SLAVEID":
                            cfg.slaveId = cfgDataStr;
                            System.IO.File.AppendAllText(filePath, @"REG=" + cfgDataStr.ToLower() + @",");
                            //g_f.Log(@"REG=" + cfgDataStr + @"," + @"\\\\\" + filePath);
                            break;
                        case "REG":
                            cfg.reg = cfgDataStr;
                            System.IO.File.AppendAllText(filePath, cfgDataStr.ToLower() + @",");
                            //g_f.Log(cfgDataStr + @"," + @"\\\\\" + filePath);
                            break;
                        case "DATA":
                            cfg.data = cfgDataStr;
                            System.IO.File.AppendAllText(filePath, cfgDataStr.ToLower() + "\r\n");
                            dataDoneFlag = true;
                            if (delayValue != null) {
                                System.IO.File.AppendAllText(filePath, @"DELAY=" + delayValue.ToLower() + "\r\n");
                                delayValue = null;
                                dataDoneFlag = false;
                            }
                            
                            break;
                        case "DATALEN":
                            cfg.dataLen = cfgDataStr;
                            break;
                        case "DELAY":
                            cfg.delay = cfgDataStr;
                            delayValue = cfgDataStr;
                            if(dataDoneFlag) {
                                System.IO.File.AppendAllText(filePath, @"DELAY=" + delayValue.ToLower() + "\r\n");
                                delayValue = null;
                                dataDoneFlag = false;
                            }
                            break;
                        default:
                            //g_f.Log("default line:" + i.ToString());
                            break;
                    }
                }
            }
            g_f.RemoveSpecifyLine(filePath, g_f.getMaxLine(filePath)); // remove last line —— \r\n
            g_f.RenameFileName(filePath);
        }

        private void TransformToBypass()
        {
            String format = " yyyy-MM-dd_hhmmssffff";
            DateTime date = DateTime.Now;
            string time = date.ToString(format, DateTimeFormatInfo.InvariantInfo);
            string filePath = g_dirPath + System.IO.Path.GetFileNameWithoutExtension(g_fileName) + time + ".ini";
            if (!System.IO.File.Exists(filePath)) {
                System.IO.File.Copy(g_dirPath + g_fileName, filePath, true);
            } else {
                //g_f.Log("err:请删除冗余同名文件" + filePath + @"[TransformToBypass]");
                System.IO.File.Copy(g_dirPath + g_fileName, filePath, true);
            }
            //g_f.Replace(filePath, filePath, "\r\n\r\n", "\r\n");
            //g_f.Replace(filePath, filePath, "\r\n\r\n", "\r\n");
            int totalLineNum = g_f.getMaxLine(g_dirPath + g_fileName);
            g_f.Clear(filePath);

            g_f.getMaxLine(g_dirPath + g_fileName);
            g_f.Log("[TransformToBypass] totalLineNum=" + totalLineNum.ToString());
            int cfgNum = 0; // 第cfgNum条有效配置
            int flagOdd = 0;
            int flagEven = 0;
            bool isUpdateDelay = false;
            CFG cfg = new CFG() { slaveId = null, reg = null, data = null, dataLen = null, delay = null };
            for (int i = 1; i <= totalLineNum; i++) {
                string currLineStr = g_f.getLine(g_dirPath + g_fileName, i).ToUpper();
                currLineStr = currLineStr.Replace(" ", ""); // move Space
                currLineStr = currLineStr.Replace("\t", ""); // move Tab
                if(currLineStr == "") {
                    g_f.Log("[TransformToBypass] 第" + i.ToString() + "行:\t" + "err: this is enter line\t" + "currLineStr=" + currLineStr);
                }
                /* 1.---- handle the comment ; ----------------------- */
                if (currLineStr.Contains(@";")) {
                    g_f.Log("11");
                    if (g_f.CutStr(currLineStr, 1, 1) == @";") {
                        g_f.Log("110");
                        continue;
                    } else {
                        int beginPosition = currLineStr.IndexOf(@";") + 1;
                        g_f.Log("111");
                        currLineStr = g_f.CutStr(currLineStr, 1, beginPosition - 1);
                        g_f.Log("112");
                    }
                }
                /* 2.---- handle the comment # ---------- ----------- */
                if (currLineStr.Contains(@"#")) {
                    int beginPosition = currLineStr.IndexOf(@"#");
                    g_f.Log("[TransformToBypass] 第" + i.ToString() + "行:\t" + "deginPosition=" + beginPosition + "\tcurrLineStr=" + currLineStr);
                    if (g_f.CutStr(currLineStr, 1, 1) == @"#") {
                        g_f.Log("[TransformToBypass] 第" + i.ToString() + "行:\t" + "deginPosition=" + beginPosition + "\tcurrLineStr=" + currLineStr);
                        currLineStr = g_f.CutStr(currLineStr, 1, beginPosition - 1);
                    }

                }
                /* 3.---- handle no the comment --------------------- */
                {
                    string signDelayStr = "DELAY=";
                    g_f.Log("31");
                    if (g_f.CutStr(currLineStr, 1, signDelayStr.Length) == signDelayStr) {
                        if (cfgNum == 0 || isUpdateDelay) {
                            continue;
                        }
                        flagEven = 1;
                        cfg.delay = currLineStr.Remove(0, signDelayStr.Length).ToLower();
                        g_f.Log("第" + i.ToString() + "行:\t" + "delay=" + cfg.delay);
                    } else {
                        cfg.delay = "0x01";
                    }
                    string signRegStr = "REG=";
                    g_f.Log("32");
                    if (g_f.CutStr(currLineStr, 1, signRegStr.Length) == signRegStr) {
                        flagOdd = 1;
                        isUpdateDelay = false;
                        int cammaOnePosition = currLineStr.IndexOf(@",") + 1;
                        int cammaTwoPosition = currLineStr.IndexOf(@",", cammaOnePosition) + 1;
                        g_f.Log("cammaOnePosition=" + cammaOnePosition + "\tcammaTwoPosition=" + cammaTwoPosition);
                        if(cammaOnePosition <= 0 || cammaTwoPosition <= 0 || cammaOnePosition >= cammaTwoPosition) {
                            continue;
                        }
                        g_f.Log("33");
                        cfg.slaveId = g_f.CutStr(currLineStr, signRegStr.Length + 1, cammaOnePosition - signRegStr.Length - 1).ToLower();
                        g_f.Log("34");
                        cfg.reg = g_f.CutStr(currLineStr, cammaOnePosition + 1, cammaTwoPosition - cammaOnePosition - 1).ToLower();
                        cfg.data = currLineStr.Remove(0, cammaTwoPosition).ToLower();
                        if(cfg.data.Contains(@"0x")) {
                            cfg.dataLen = ((cfg.data.Length - 1) / 2).ToString();
                        } else {
                            cfg.dataLen = ((cfg.data.Length + 1) / 2).ToString();
                        }
                        g_f.Log("第" + i.ToString() + "行:\t" + @"第1个','=" + cammaOnePosition.ToString() + "第2个','=" + cammaTwoPosition + "\tslaveId=" + cfg.slaveId + "\treg=" + cfg.reg + "\tdata=" + cfg.data + "\tdataLen=" + cfg.dataLen);

                    }
                    /* ↓↓↓↓↓↓↓↓ --  Verification -- ↓↓↓↓↓↓*/
                    if(flagOdd == 0 && flagEven == 0) {
                        continue;
                    } else {
                        g_f.Log("35");
                        if(flagEven != 0) {
                            if (cfg.delay.Contains(@";")) {
                                cfg.delay = g_f.CutStr(cfg.delay, 0, cfg.delay.IndexOf(";") - 1);
                            }
                            g_f.Log("351");
                            if (cfg.delay.Contains(@"#")) {
                                cfg.delay = g_f.CutStr(cfg.delay, 0, cfg.delay.IndexOf("#") - 1);
                            }
                            g_f.Log("352");
                        }
                        if(flagOdd != 0) {
                            if (cfg.data.Contains(@";")) {
                                cfg.data = g_f.CutStr(cfg.data, 0, cfg.data.IndexOf(";") - 1);
                            }
                            g_f.Log("353");
                            if (cfg.data.Contains(@"#")) {
                                cfg.data = g_f.CutStr(cfg.data, 0, cfg.data.IndexOf("#") - 1);
                            }
                        }
                    }
                    g_f.Log("36");
                    /* ↑↑↑↑↑↑↑↑ --  Verification -- ↑↑↑↑↑↑*/

                    /* ↓↓↓↓↓↓↓↓ --  Generate -- ↓↓↓↓↓↓*/
                    if (flagOdd != 0 || flagEven != 0) {
                        g_f.Log("40");
                        if (flagEven != 0) {
                            if(cfgNum == 0) {
                                g_f.Log("41");
                                continue;
                            } else {
                                g_f.Log("42");
                                cfgNum--;
                            }
                            int delayLine = cfgNum * 6 + 1;
                            string crruCfgStr = g_f.getLine(filePath, delayLine);
                            if(cfgNum == 0) {
                                g_f.Clear(filePath);
                                g_f.Log("43");
                            } else {
                                g_f.Log("44");
                                for (int num = 0; num <= 5; num++) {
                                    g_f.Log("第" + (delayLine + 4 - num).ToString() + "行：" + "[" + cfgNum + "]" + crruCfgStr);
                                    g_f.RemoveSpecifyLine(filePath, delayLine + 4 - num);
                                }
                            }
                            flagEven = 0;
                            isUpdateDelay = true;
                            g_f.Log("第" + delayLine.ToString() + "行：" + "[" + cfgNum + "]" + crruCfgStr);
                        }
                        System.IO.File.AppendAllText(filePath, @"    configData[" + cfgNum.ToString() + @"].words.delay = " + cfg.delay + ";\r\n");
                        System.IO.File.AppendAllText(filePath, @"    configData[" + cfgNum.ToString() + @"].words.slaveId = " + cfg.slaveId + ";\r\n");
                        System.IO.File.AppendAllText(filePath, @"    configData[" + cfgNum.ToString() + @"].words.reg = " + cfg.reg + ";\r\n");
                        System.IO.File.AppendAllText(filePath, @"    configData[" + cfgNum.ToString() + @"].words.data = " + cfg.data + ";\r\n");
                        System.IO.File.AppendAllText(filePath, @"    configData[" + cfgNum.ToString() + @"].words.dataLen = " + cfg.dataLen + ";\r\n");
                        System.IO.File.AppendAllText(filePath, "\r\n");
                        if(flagEven != 0) {
                            cfg = new CFG() { slaveId = null, reg = null, data = null, dataLen = null, delay = null };
                        }
                        cfgNum++;
                        flagOdd = 0;
                    }
                    /* ↑↑↑↑↑↑↑↑ --  Generate -- ↑↑↑↑↑↑*/
                    
                }
            }
            g_f.RemoveSpecifyLine(filePath, g_f.getMaxLine(filePath)); // remove last line —— \r\n
            g_f.RemoveSpecifyLine(filePath, g_f.getMaxLine(filePath)); // remove last line —— \r\n
            g_f.RenameFileName(filePath);
        }

        private bool TransformStart()
        {
            int cfgType = PickOutTransformType();
            if (cfgType == (int)cfg_type.VIO4) {
                TransformToBypass();
            } else if(cfgType == (int)cfg_type.BYPASS) {
                TransformToVio4();
            } else {
                g_f.Log("[TransformStart] Error：文件内容配置有误，type=" + cfgType);
                MessageBox.Show("Error：文件内容配置有误 " + cfgType, "TransformStart");
                return false;
            }
            return true;
        }

        public int PickOutTransformType()
        {
            int cfgType = (int)cfg_type.MAX_TYPE;
            int totalLineNum = 0;
            if (g_dirPath == null) {
                g_dirPath = @".\";
                totalLineNum = g_f.getMaxLine(@".\" + g_fileName);
            } else {
                totalLineNum = g_f.getMaxLine(g_dirPath + g_fileName);
            }
            if (totalLineNum <= 0) {
                g_f.Log("[PickOutTransformType] err:文件内容获取fail path:" + g_dirPath);
                MessageBox.Show("err:文件内容获取fail", "PickOutTransformType");
                return cfgType;
            }

            bool isTypeVio4 = true;
            bool isTypeBypass = true;
            for (int i = 1; i <= totalLineNum; i++) {
                string currLineStr = g_f.getLine(g_dirPath + g_fileName, i);
                char[] charsToTrim = { ' ', '\t' };
                currLineStr = currLineStr.Trim(charsToTrim).ToUpper(); // remove Space、Tab
                //g_f.Log("行号:" + i.ToString() + "\t str:" + currLineStr);
                //g_f.Log(i.ToString() + "行:" + System.Text.RegularExpressions.Regex.Replace(currLineStr, @"\d", ""));
                //g_f.Log("[PickOutTransformType] line:" + i.ToString() + "\tsign=" + g_signBypass.Remove(10) + "\tcurrLineStr=" + currLineStr);
                if(currLineStr.Contains(g_signBypass.Remove(10)) && currLineStr != "" || currLineStr.Length <= 0) {
                    //g_f.Log("bypass sign:" + g_f.CutStr(currLineStr, 1, 10));
                } else {
                    isTypeBypass = false;
                }
                if ((currLineStr.Contains("REG=") || currLineStr.Contains("DELAY=")) || currLineStr == "" || currLineStr.Length <= 0 ||
                    g_f.CutStr(currLineStr, 1, 1) == "#" || g_f.CutStr(currLineStr, 1, 1) == ";") {
                } else {
                    //g_f.Log("[PickOutTransformType] 第" + i.ToString() + "行:\t" + "currLineStr=" + currLineStr);
                    isTypeVio4 = false;
                }
            }
            if (cfgType == (int)cfg_type.MAX_TYPE) {
                if (isTypeVio4) {
                    cfgType = (int)cfg_type.VIO4;
                }
                if (isTypeBypass) {
                    cfgType = (int)cfg_type.BYPASS;
                }
            }
            g_f.Log("[PickOutTransformType] cfgType=" + cfgType.ToString());
            return cfgType;
        }

    }

}
