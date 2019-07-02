using System;
using System.IO;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml;

// https://social.msdn.microsoft.com/Forums/vstudio/en-US/16de05ed-3776-40e5-b576-139603e4b374/the-incoming-message-was-signed-with-a-token-which-was-different-from-what-used-to-encrypt-the-body?forum=wcf

namespace WsAncertConnection.NetFramework.Bindings.CustomTextMessage
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


        public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
        {
            var bufferContents = new byte[buffer.Count];
            var bufferArray = buffer.Array ?? throw new InvalidOperationException();

            Array.Copy(bufferArray, buffer.Offset, bufferContents, 0, bufferContents.Length);
            bufferManager.ReturnBuffer(buffer.Array);

            var stream = new MemoryStream(bufferContents);
            return ReadMessage(RemoveEnvelope(stream), int.MaxValue);
        }

        public static Stream RemoveEnvelope(Stream input)
        {
            using (var streamReader = new StreamReader(input))
            {
                var xml = streamReader.ReadToEnd();

                var headerFirstIndex = xml.IndexOf("Header", StringComparison.Ordinal);
                if (headerFirstIndex <= 0) return input;

                var headerLastIndex = xml.LastIndexOf("Header", StringComparison.Ordinal);

                var start = xml.LastIndexOf("<", headerFirstIndex, StringComparison.Ordinal);
                var end = xml.IndexOf(">", headerLastIndex, StringComparison.Ordinal) + 1;

                xml = xml.Substring(0, start) + xml.Substring(end);
                return new MemoryStream(new UTF8Encoding().GetBytes(xml));
            }
        }

        public override Message ReadMessage(Stream stream, int maxSizeOfHeaders, string contentType)
        {
            var reader = XmlReader.Create(stream);
            return Message.CreateMessage(reader, maxSizeOfHeaders, MessageVersion);
        }

        public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
        {
            var stream = new MemoryStream();
            var writer = XmlWriter.Create(stream, _writerSettings);
            message.WriteMessage(writer);
            writer.Close();

            var messageBytes = stream.GetBuffer();
            var messageLength = (int)stream.Position;
            stream.Close();

            var totalLength = messageLength + messageOffset;
            var totalBytes = bufferManager.TakeBuffer(totalLength);
            Array.Copy(messageBytes, 0, totalBytes, messageOffset, messageLength);

            var byteArray = new ArraySegment<byte>(totalBytes, messageOffset, messageLength);
            return byteArray;
        }

        public override void WriteMessage(Message message, Stream stream)
        {
            var writer = XmlWriter.Create(stream, _writerSettings);
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
