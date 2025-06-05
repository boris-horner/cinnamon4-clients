using ICSharpCode.SharpZipLib.Zip;

namespace CDCplusLib.Helpers
{
    public class ZipHelper
    {
        public static void ZipFolder(string sourceFolder, string zipFn)
        {
            ZipOutputStream outStream = new ZipOutputStream(File.Create(zipFn));
            try
            {
                outStream.SetLevel(9);
                ZipEntryFactory zef = new ZipEntryFactory();

                // folders
                foreach (string folderName in Directory.GetDirectories(sourceFolder, "*", SearchOption.AllDirectories))
                {
                    ZipEntry ze = zef.MakeDirectoryEntry(folderName.Substring(sourceFolder.Length));
                    outStream.PutNextEntry(ze);
                }

                // files
                foreach (string inFn in Directory.GetFiles(sourceFolder, "*", SearchOption.AllDirectories))
                {
                    FileStream inStream = File.OpenRead(inFn);
                    try
                    {
                        ZipEntry ze = zef.MakeFileEntry(inFn.Substring(sourceFolder.Length));
                        ze.Size = inStream.Length;
                        outStream.PutNextEntry(ze);
                        inStream.CopyTo(outStream);
                    }
                    finally
                    {
                        inStream.Close();
                    }
                }
                outStream.Finish();
            }
            finally
            {
                outStream.Close();
            }
        }
        public static void UnzipFolder(string zipFn, string targetFolder)
        {
            ZipInputStream inStream = new ZipInputStream(File.OpenRead(zipFn));
            ZipEntry ze = inStream.GetNextEntry();
            while (ze != null)
            {
                if (ze.IsFile)
                {
                    if (ze.CanDecompress)
                    {
                        string outFn = Path.Combine(targetFolder, ze.Name.Replace('/', Path.DirectorySeparatorChar));
                        string outPath = Path.GetDirectoryName(outFn);
                        if (!Directory.Exists(outPath)) Directory.CreateDirectory(outPath);
                        FileStream outStream = new FileStream(outFn, FileMode.Create);
                        inStream.CopyTo(outStream);
                        outStream.Close();
                    }
                }
                else if (ze.IsDirectory)
                {
                    string outPath = Path.Combine(targetFolder, ze.Name.Replace('/', Path.DirectorySeparatorChar));
                    if (!Directory.Exists(outPath)) Directory.CreateDirectory(outPath);
                }
                ze = inStream.GetNextEntry();
            }
            inStream.Close();
        }
    }
}
