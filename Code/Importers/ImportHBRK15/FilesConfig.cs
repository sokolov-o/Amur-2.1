using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FERHRI.Amur.Importer
{
    internal class FilesConfig
    {
        static private string XML_PATH = "Files.config";
        internal List<FileType> FileTypes = new List<FileType>();

        FilesConfig() { }

        static public FilesConfig Parse()
        {
            FilesConfig ret = new FilesConfig();
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(XML_PATH);

            foreach (XmlNode xn in xmldoc.DocumentElement.GetElementsByTagName("fileType"))
            {
                FileType fileType = new FileType()
                {
                    Type = xn.Attributes["name"].Value,
                    Dir = xn.Attributes["dirImport"].Value,
                    LogFile = new LogFile(xn.Attributes["dirImport"].Value, xn.Attributes["logFile"].Value),
                    SubDir2Keep = xn.Attributes["subDir2Keep"].Value,
                    DataType = xn.Attributes["dataType"].Value,
                    FileSubTypes = new List<FileSubType>()
                };
                ret.FileTypes.Add(fileType);

                foreach (XmlNode xn1 in xn)
                {
                    if (xn1.NodeType != XmlNodeType.Comment)
                    {
                        fileType.FileSubTypes.Add(new FileSubType()
                        {
                            Type = xn1.Attributes["name"].Value,
                            FileNames = new List<string>(xn1.Attributes["fileNames"].Value.Split(';'))
                        });
                    }
                }
            }
            return ret;
        }

        //static public void KeepFile(FileInfo fileInfo, string subDir2Keep)
        //{
        //    string keepDirPath = fileInfo.DirectoryName + "\\" + subDir2Keep + "\\";
        //    string keepFilePath = keepDirPath + (fileInfo.Name.Replace(fileInfo.Extension, "") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileInfo.Extension);

        //    if (!Directory.Exists(keepDirPath))
        //        Directory.CreateDirectory(keepDirPath);
        //    File.Copy(fileInfo.FullName, keepFilePath);
        //}
    }

    internal class FileType
    {
        internal string Type { get; set; }
        internal string Dir { get; set; }
        internal string SubDir2Keep { get; set; }
        /// <summary>
        /// forecasts or observations
        /// </summary>
        internal string DataType { get; set; }
        internal List<FileSubType> FileSubTypes { get; set; }

        internal LogFile LogFile { get; set; }

        public void KeepFile(FileInfo fileInfo)
        {
            string keepDirPath = fileInfo.DirectoryName + "\\" + SubDir2Keep + "\\";
            string keepFilePath = keepDirPath + (fileInfo.Name.Replace(fileInfo.Extension, "") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileInfo.Extension);

            if (!Directory.Exists(keepDirPath))
                Directory.CreateDirectory(keepDirPath);
            File.Copy(fileInfo.FullName, keepFilePath);
        }

    }

    internal class FileSubType
    {
        internal string Type { get; set; }
        internal List<string> FileNames { get; set; }
    }
}
