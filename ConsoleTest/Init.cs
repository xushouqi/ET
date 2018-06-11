using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using ETModel;

namespace ConsoleTest
{
    public class Init
    {
        private readonly OneThreadSynchronizationContext contex = new OneThreadSynchronizationContext();

        public async Task Start()
        {
            Console.WriteLine("Init.Start");
            SynchronizationContext.SetSynchronizationContext(this.contex);

            Game.EventSystem.Add(DLLType.Model, typeof(ETModel.Init).Assembly);
            Game.EventSystem.Add(DLLType.Hotfix, typeof(Init).Assembly);

            Game.Scene.AddComponent<NetOuterComponent>();
            Game.Scene.AddComponent<ResourcesComponent>();
            Game.Scene.AddComponent<PlayerComponent>();
            Game.Scene.AddComponent<UnitComponent>();
            Game.Scene.AddComponent<ClientFrameComponent>();


            // 加载配置
            //Game.Scene.GetComponent<ResourcesComponent>().LoadBundle("config.unity3d");
            //Game.Scene.AddComponent<ConfigComponent>();
            //Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle("config.unity3d");
            Game.Scene.AddComponent<OpcodeTypeComponent>();
            Game.Scene.AddComponent<MessageDispatherComponent>();


            IPEndPoint connetEndPoint = NetworkHelper.ToIPEndPoint("127.0.0.1:10002");

            // 创建一个ETModel层的Session
            var session = ETModel.Game.Scene.GetComponent<NetOuterComponent>().Create(connetEndPoint);
            // 创建一个ETHotfix层的Session, ETHotfix的Session会通过ETModel层的Session发送消息
            //var realmSession = ComponentFactory.Create<ETModel.Session, ETModel.Session>(session);
            var r2CLogin = (R2C_Login)await session.Call(new C2R_Login() { Account = "aaa", Password = "111111" });
            session.Dispose();

            Console.WriteLine("Login Success!");

            connetEndPoint = NetworkHelper.ToIPEndPoint(r2CLogin.Address);
            // 创建一个ETModel层的Session,并且保存到ETModel.SessionComponent中
            ETModel.Session gateSession = ETModel.Game.Scene.GetComponent<NetOuterComponent>().Create(connetEndPoint);
            ETModel.Game.Scene.AddComponent<ETModel.SessionComponent>().Session = gateSession;

            // 创建一个ETHotfix层的Session, 并且保存到ETHotfix.SessionComponent中
            Game.Scene.AddComponent<SessionComponent>().Session = ComponentFactory.Create<Session, ETModel.Session>(gateSession);

            G2C_LoginGate g2CLoginGate = (G2C_LoginGate)await SessionComponent.Instance.Session.Call(new C2G_LoginGate() { Key = r2CLogin.Key });

            Console.WriteLine("Connect Gate Success!");

            // 创建Player
            Player player = ETModel.ComponentFactory.CreateWithId<Player>(g2CLoginGate.PlayerId);
            PlayerComponent playerComponent = ETModel.Game.Scene.GetComponent<PlayerComponent>();
            playerComponent.MyPlayer = player;

        }
    }
}
