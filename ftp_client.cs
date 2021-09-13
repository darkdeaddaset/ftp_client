using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;

namespace ftp_client
{
    class ftp_client
    {
        private string host = null;
        private string username = null;
        private string password = null;
        private FtpWebRequest ftpRequest = null;
        private FtpWebResponse ftpResponse = null;
        private Stream ftpStream = null;
        private int bufferSize = 2048;

        public ftp_client(string hostIP, string UserName, string Password)
        {
            host = hostIP;
            username = UserName;
            password = Password;
        }

        public void Download(string remoteFile, string localFile)
        {
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + remoteFile);
                ftpRequest.Credentials = new NetworkCredential(username, password);
                ftpRequest.KeepAlive = true;
                ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                ftpStream = ftpResponse.GetResponseStream();
                FileStream localFileStream = new FileStream(localFile, FileMode.Open);
                byte[] byteBuffer = new byte[bufferSize];
                int byteRead = ftpStream.Read(byteBuffer, 0, bufferSize);
                
                while (byteRead > 0)
                {
                    localFileStream.Write(byteBuffer, 0, byteRead);
                    byteRead = ftpStream.Read(byteBuffer, 0, bufferSize);
                }                               
                localFileStream.Close();
                ftpStream.Close();
                ftpResponse.Close();
                ftpRequest = null;            
        }

        public void Upload(string remoteFile, string localFile)
        {
            ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + remoteFile);
            ftpRequest.Credentials = new NetworkCredential(username, password);
            ftpRequest.KeepAlive = true;
            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
            ftpStream = ftpRequest.GetRequestStream();
            FileStream localFileStream = new FileStream(localFile, FileMode.Open);
            byte[] byteBuffer = new byte[bufferSize];
            int byteSent = localFileStream.Read(byteBuffer, 0, bufferSize);

            while (byteSent > 0)
            {
                ftpStream.Write(byteBuffer, 0, byteSent);
                byteSent = localFileStream.Read(byteBuffer, 0, bufferSize);
            }
            localFileStream.Close();
            ftpStream.Close();
            ftpRequest = null;
        }

        public void Delete(string deleteFile)
        {
            ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + deleteFile);
            ftpRequest.Credentials = new NetworkCredential(username, password);
            ftpRequest.KeepAlive = true;
            ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            ftpResponse.Close();
            ftpRequest = null;
        }

        public void DeleteDir(string deleteDir)
        {
            ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + deleteDir);
            ftpRequest.Credentials = new NetworkCredential(username, password);
            ftpRequest.KeepAlive = true;
            ftpRequest.Method = WebRequestMethods.Ftp.RemoveDirectory;
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            ftpResponse.Close();
            ftpRequest = null;
        }

        public void Rename(string currentFileNameAndPath, string newFileName)
        {
            ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + currentFileNameAndPath);
            ftpRequest.Credentials = new NetworkCredential(username, password);
            ftpRequest.KeepAlive = true;
            ftpRequest.Method = WebRequestMethods.Ftp.Rename;
            ftpRequest.RenameTo = newFileName;
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            ftpResponse.Close();
            ftpRequest = null;
        }

        public void createDirectory(string newDirectory)
        {
            ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + newDirectory);
            ftpRequest.Credentials = new NetworkCredential(username, password);
            ftpRequest.KeepAlive = true;
            ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            ftpResponse.Close();
            ftpRequest = null;
        }

        public string[] directoryListDetails(string directory)
        {
            ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + directory);
            ftpRequest.Credentials = new NetworkCredential(username, password);
            ftpRequest.KeepAlive = true;
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            ftpStream = ftpResponse.GetResponseStream();
            StreamReader ftpReader = new StreamReader(ftpStream);
            string readerDirectory = null;
            
            while (ftpReader.Peek() != -1)
            {
                readerDirectory += ftpReader.ReadLine() + "|";
            }

            ftpReader.Close();
            ftpStream.Close();
            ftpResponse.Close();
            ftpRequest = null;

            string[] directoryList = readerDirectory.Split("|".ToCharArray());
            return directoryList;
        }
    }
}
