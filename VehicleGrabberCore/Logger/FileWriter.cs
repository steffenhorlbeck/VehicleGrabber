using System.Text;
using System.IO;
using System.Threading;

namespace VehicleGrabberCore.Logger
{


    public class FileWriter
    {
        private ReaderWriterLockSlim lock_ = new ReaderWriterLockSlim();
        public void WriteData(string Logfile, string text, bool append = true)
        {
            this.lock_.EnterWriteLock();
            try
            {
                using (StreamWriter Writer = new StreamWriter(Logfile, append, Encoding.UTF8))
                {
                    //Writer.AutoFlush = true;
                    if (text != string.Empty)
                    {
                        Writer.WriteLine(text);
                        Writer.Flush();
                    }

                    Writer.Close();
                }
            }
            finally
            {
                this.lock_.ExitWriteLock();
            }
        }

    } // eo class FileWriter
}
