using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace SFC_Tools.Classes
{
    class DelSpecifiedFiles
    {
        private string m_strPath;
        public DelSpecifiedFiles(string strPath)
        {
            m_strPath = strPath;
        }
        public void delFiles()
        {
            SearchSubFolders(m_strPath);
            //this.SearchSubFiles(m_strPath);
        }
        private void SearchSubFolders(string strPath)
        {
            DirectoryInfo di = new DirectoryInfo(strPath);
            foreach (DirectoryInfo subDi in di.GetDirectories())
            {
                DirectoryInfo diNextFolder = di.CreateSubdirectory(subDi.Name);
                if (diNextFolder.Name.ToUpper() == "DEBUG" || diNextFolder.Name.ToUpper() == "RELEASE")
                {
                    SearchSubFiles(diNextFolder.FullName);
                }
                SearchSubFolders(diNextFolder.FullName);
            }
        }
        private void SearchSubFiles(string strPath)
        {
            
            {
                DirectoryInfo dif = new DirectoryInfo(strPath);
                foreach (FileInfo fi in dif.GetFiles())
                {
                    if (fi.Name.ToUpper().IndexOf(".LIB")>=0)
                        continue;
                    if (fi.Name.ToUpper().IndexOf(".EXE") >= 0)
                        continue;
                    if (fi.Name.ToUpper().IndexOf(".DLL") >= 0)
                        continue;
                    File.Delete(fi.FullName);
                    //MessageBox.Show(fi.Name);
                    //SearchSubFiles(diNextFolder.ToString());
                }
            }
        }
    }
}
