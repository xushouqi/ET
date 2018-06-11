using ProtoBuf;
using ETModel;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace ConsoleTest
{
    [Message(Opcode.C2R_Login)]
    [ProtoContract]
    public partial class C2R_Login : IRequest
    {
        [ProtoMember(90, IsRequired = true)]
        public int RpcId { get; set; }

        [ProtoMember(1, IsRequired = true)]
        public string Account;

        [ProtoMember(2, IsRequired = true)]
        public string Password;

    }

    [Message(Opcode.R2C_Login)]
    [ProtoContract]
    public partial class R2C_Login : IResponse
    {
        [ProtoMember(90, IsRequired = true)]
        public int RpcId { get; set; }

        [ProtoMember(91, IsRequired = true)]
        public int Error { get; set; }

        [ProtoMember(92, IsRequired = true)]
        public string Message { get; set; }

        [ProtoMember(1, IsRequired = true)]
        public string Address;

        [ProtoMember(2, IsRequired = true)]
        public long Key;

    }


    [Message(Opcode.C2G_LoginGate)]
    [ProtoContract]
    public partial class C2G_LoginGate : IRequest
    {
        [ProtoMember(90, IsRequired = true)]
        public int RpcId { get; set; }

        [ProtoMember(1, IsRequired = true)]
        public long Key;

    }

    [Message(Opcode.G2C_LoginGate)]
    [ProtoContract]
    public partial class G2C_LoginGate : IResponse
    {
        [ProtoMember(90, IsRequired = true)]
        public int RpcId { get; set; }

        [ProtoMember(91, IsRequired = true)]
        public int Error { get; set; }

        [ProtoMember(92, IsRequired = true)]
        public string Message { get; set; }

        [ProtoMember(1, IsRequired = true)]
        public long PlayerId;

    }
}
