using System;
using Bond;
using Bond.IO.Unsafe;
using Bond.Protocols;

namespace GrpcSample.Shared
{
    public static class Serializer<T>
    {
        public static byte[] ToBytes(T obj)
        {
            var buffer = new OutputBuffer();
            var writer = new FastBinaryWriter<OutputBuffer>(buffer);
            Serialize.To(writer, obj);
            var output = new byte[buffer.Data.Count];
            Array.Copy(buffer.Data.Array, 0, output, 0, (int)buffer.Position);
            return output;
        }

        public static T FromBytes(byte[] bytes)
        {
            var buffer = new InputBuffer(bytes);
            var data = Deserialize<T>.From(new FastBinaryReader<InputBuffer>(buffer));
            return data;
        }
    }
}