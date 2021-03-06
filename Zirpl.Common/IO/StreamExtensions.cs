﻿using System;
using System.IO;

namespace Zirpl.IO
{
    public static class StreamExtensions
    {
        public static String ReadAllToString(this Stream stream)
        {
            String contents = null;
            using (var reader = new StreamReader(stream))
            {
                contents = reader.ReadToEnd();
            }
            return contents;
        }
        
        public static byte[] ReadAllToBytes(this Stream stream)
        {
            Int32 length = (Int32)stream.Length;
            var buffer = new byte[length];
            stream.Read(buffer, 0, length);
            return buffer;
        }


        public static void WriteText(this Stream stream, String text)
        {
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(text);
                writer.Flush();
            }
        }

        public static void CopyStream(this Stream from, Stream to)
        {
            if (from == null)
            {
                throw new ArgumentNullException("from");
            }
            if (to == null)
            {
                throw new ArgumentNullException("to");
            }

            byte[] buffer = new byte[8192];

            int bytesRead;
            while ((bytesRead = from.Read(buffer, 0, buffer.Length)) > 0)
            {
                to.Write(buffer, 0, bytesRead);
            }
        }
        
    }
}
