using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Wspolbiezne.Data
{
    public class Logger
    {
        private const string fileName = @".\logs.json";

        private static ReaderWriterLockSlim readerWriterLock = new ReaderWriterLockSlim();

        private JsonSerializerOptions serializerOptions = new JsonSerializerOptions() { WriteIndented = true };

        public Logger()
        {
            File.Create(fileName);
        }

        public async void LogCollisionData(List<CollisionData> collisions)
        {
            if (collisions.Count != 0)
            {
                readerWriterLock.EnterWriteLock();
                try
                {
                    using (FileStream sourceStream = File.Open(fileName, FileMode.Open))
                    {
                        sourceStream.Seek(0, SeekOrigin.End);
                        byte[] result = Encoding.Unicode.GetBytes(JsonSerializer.Serialize(collisions, serializerOptions));
                        await sourceStream.WriteAsync(result);
                    }
                }
                finally
                {
                    readerWriterLock.ExitWriteLock();
                }
            }
        }

        public async void LogBallPosition(BallData ballData)
        {
            readerWriterLock.EnterWriteLock();
            try
            {
                using (FileStream sourceStream = File.Open(fileName, FileMode.Open))
                {
                    sourceStream.Seek(0, SeekOrigin.End);
                    byte[] result = Encoding.Unicode.GetBytes(JsonSerializer.Serialize(ballData, serializerOptions));
                    await sourceStream.WriteAsync(result);
                }
            }
            finally
            {
                readerWriterLock.ExitWriteLock();
            }
        }
    }
}
