/*
 * Create Date：2011-12-22
 * Author：Rain.Liu
 * Description：This Class Is used to get the next string，whatever your base string is ok
 * Parameters：Base Input
 * Output：string
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SFC_Tools
{
    class GetNextXChar
    {
        private string strOra;
        private string strBase;
        private string strTopLevel;

        public string GetNextXChars(string strParam, ArrayList arrBase, int iStep)
        {
            string strValue=string.Empty;
            for (int i = 0; i < arrBase.Count; i++)
            {
                   
            }
            return strValue;
        }

        public string GetNextXCharByByte(string strParamOra, string strParamBase, int iStep = 1)
        {
            string strRetVal=string.Empty;
            if (strParamOra.Trim().Length == 0 || strParamBase.Trim().Length == 0)
            {
                throw new Exception("Base string And Input string should not be null!");
            }
            else
            {
                this.strTopLevel = strParamBase.Substring(strParamBase.Length - 1, 1);
                this.strOra = strTopLevel + strParamOra;
                this.strBase = strParamBase;
            }

            strRetVal = GetChar(iStep);
            return strRetVal;
        }
        public string GetChar(decimal iFlag)
        {
            string strRes = "";
            decimal dTemp = EeCodeInput(strOra, strBase);
            if (dTemp < 0)
            {
                throw new Exception("Error,Your data not match to the Base string!");
            }
            strRes = DeCode(dTemp, strBase, iFlag);
            string strTemp = strRes.Substring(0, 1);
            if (strTemp != this.strTopLevel)
            {
                throw new Exception("Error,Over flow happen,Please reset your End SN!");
            }
            strRes = strRes.Remove(0, 1);
            return strRes;     
        }
        //get teh code encode
        private decimal EeCodeInput(string strParam, string strBase)
        {
            decimal decMideRes = 0;
            int iLen = strParam.Length;
            int iBaseLen = strBase.Length;
            for (int i = 1; i <= iLen; i++)
            {
                string ch = strParam.Substring(i - 1, 1);
                decimal dStep = strBase.IndexOf(ch);
                if (dStep < 0)
                {
                    return -1;
                }
                int k = 1;
                while (k <= (iLen - i))
                {
                    dStep = dStep * iBaseLen;
                    k++;
                }
                decMideRes = decMideRes + dStep;
            }
            return decMideRes;
        }
        //Translate the code into string
        private string DeCode(decimal dParam, string strBase, decimal iPlus)
        {
            string strRes = "";
            decimal dInput = dParam + iPlus;
            while (dInput > 0)
            {
                decimal dInt = dInput % strBase.Length;
                int iTemp = int.Parse(dInt.ToString());
                string strTemp = strBase.Substring(iTemp, 1);
                dInput = Math.Truncate(dInput / strBase.Length);
                strRes = strTemp + strRes;
            }
            return strRes;
        }
    }
    /// <summary>
    /// Created by Rian.liu
    /// Version 1.1
    /// Date:2012-8-10 11:15:29
    /// Mark:used to get the Target string from  not only 10 bit data,but also whatever base string.
    /// </summary>
    class RinGetXChar
    {
        //used for the condition that only use one same Base string with all characters
        public string GetNextXChar(string strInput, string strBase,int iStep=1)
        {
            string strRet = string.Empty;  
            char[] chInput;
            chInput = strInput.ToCharArray();
            int iForward = iStep;
            int iBaseLen = strBase.Length;
            for (int i = strInput.Length - 1; i >= 0; i--)
            { 
                int iPos=0;
                iPos = strBase.IndexOf(chInput[i]);
                if (iPos < 0)
                {
                    return("ERROR-0x000001:"+"Base String Error!");
                }
                iPos = iPos + iForward;
                iForward = iPos / iBaseLen;
                iPos = iPos % iBaseLen;
                chInput[i]=strBase[iPos];
            }
            if (iForward > 0)
            {
                return ("ERROR-0x000002" + "Over Flow Happen,Pelease input the right string!");
            }
            strRet = new string(chInput);
            return strRet;
        }
        /// <summary>
        /// The function is used for the very complicated condition that every byte have different base string
        /// </summary>
        /// <param name="strInput">Source string</param>
        /// <param name="arrBase">Base String array</param>
        /// <param name="iStep">step</param>
        /// <returns>Target string</returns>
        public string GetNextXChar(string strInput, ArrayList arrBase, int iStep = 1)
        {
            if(strInput.Length!=arrBase.Count)
            {
                return ("ERROR-0x000001:"+"Base String Error!");
            }
            string strRet = string.Empty;
            char[] chInput;
            chInput = strInput.ToCharArray();
            int iForward = iStep;
            int iBaseLen=0;
            for (int i = strInput.Length - 1; i >= 0; i--)
            {
                iBaseLen = arrBase[i].ToString().Length;
                int iPos = 0;
                iPos = arrBase[i].ToString().IndexOf(chInput[i]);
                if (iPos < 0)
                {
                    return ("ERROR-0x000002:" + "Base String Error!");
                }
                iPos = iPos + iForward;
                iForward = iPos / iBaseLen;
                iPos = iPos % iBaseLen;
                chInput[i] = arrBase[i].ToString()[iPos];
            }
            if (iForward > 0)
            {
                return ("ERROR-0x000003" + "Over Flow Happen,Pelease input the right string!");
            }
            strRet = new string(chInput);
            return strRet;
        }
    }
}
