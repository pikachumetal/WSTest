using System;
using System.IO;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml;

// https://social.msdn.microsoft.com/Forums/vstudio/en-US/16de05ed-3776-40e5-b576-139603e4b374/the-incoming-message-was-signed-with-a-token-which-was-different-from-what-used-to-encrypt-the-body?forum=wcf

namespace WsAncertCommunication.Bindings.CustomTextMessage
{
    public class CustomTextMessageEncoder : MessageEncoder
    {
        private readonly CustomTextMessageEncoderFactory _factory;
        private readonly XmlWriterSettings _writerSettings;

        public override string ContentType { get; }

        public override string MediaType => _factory.MediaType;
        public override MessageVersion MessageVersion => _factory.MessageVersion;

        public CustomTextMessageEncoder(CustomTextMessageEncoderFactory factory)
        {
            _factory = factory;
            _writerSettings = new XmlWriterSettings { Encoding = Encoding.GetEncoding(factory.CharSet) };
            ContentType = $"{_factory.MediaType}; charset={_writerSettings.Encoding.HeaderName}";
        }

        //public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
        //{
        //    byte[] msgContents = new byte[buffer.Count];
        //    Array.Copy(buffer.Array, buffer.Offset, msgContents, 0, msgContents.Length);
        //    bufferManager.ReturnBuffer(buffer.Array);

        //    MemoryStream stream = new MemoryStream(msgContents);
        //    return ReadMessage(stream, int.MaxValue);
        //}

        public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
        {
            byte[] msgContents = new byte[buffer.Count];
            Array.Copy(buffer.Array, buffer.Offset, msgContents, 0, msgContents.Length);
            bufferManager.ReturnBuffer(buffer.Array);

            MemoryStream stream = new MemoryStream(msgContents);
            return ReadMessage(RemoveEnvelope(stream), int.MaxValue);
        }

        public static Stream RemoveEnvelope(Stream input)
        {
            // Cannot process buffered -- read it all into a string
            using (var streamReader = new StreamReader(input))
            {
                var xml = streamReader.ReadToEnd();
                // Process the string using the XmlReader class, since it is complex to parse strings with XML namespaces...
                // XmlReader xr = XmlReader.Create(new StringReader(xml));

                // Seek the elements we need to modify
                int envelope = 0;
                if ((envelope = xml.IndexOf("Header")) > 0)
                {
                    int start = xml.LastIndexOf("<", envelope);
                    int end = xml.LastIndexOf("Header");
                    end = xml.IndexOf(">", end) + 1;
                    xml = xml.Substring(0, start) + xml.Substring(end);
                    MemoryStream ms = new MemoryStream(new UTF8Encoding().GetBytes(xml));
                    return ms;
                }
                return input;
            }
        }

        public override Message ReadMessage(Stream stream, int maxSizeOfHeaders, string contentType)
        {
            XmlReader reader = XmlReader.Create(stream);
            return Message.CreateMessage(reader, maxSizeOfHeaders, MessageVersion);
        }

        public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
        {
            MemoryStream stream = new MemoryStream();
            XmlWriter writer = XmlWriter.Create(stream, _writerSettings);
            message.WriteMessage(writer);
            writer.Close();

            byte[] messageBytes = stream.GetBuffer();
            int messageLength = (int)stream.Position;
            stream.Close();

            int totalLength = messageLength + messageOffset;
            byte[] totalBytes = bufferManager.TakeBuffer(totalLength);
            Array.Copy(messageBytes, 0, totalBytes, messageOffset, messageLength);

            ArraySegment<byte> byteArray = new ArraySegment<byte>(totalBytes, messageOffset, messageLength);
            return byteArray;
        }

        public override void WriteMessage(Message message, Stream stream)
        {
            XmlWriter writer = XmlWriter.Create(stream, _writerSettings);
            message.WriteMessage(writer);
            writer.Close();
        }

        public override bool IsContentTypeSupported(string contentType)
        {
            // TODO: Make something better
            //return base.IsContentTypeSupported(contentType);
            return true;
        }
    }
}
