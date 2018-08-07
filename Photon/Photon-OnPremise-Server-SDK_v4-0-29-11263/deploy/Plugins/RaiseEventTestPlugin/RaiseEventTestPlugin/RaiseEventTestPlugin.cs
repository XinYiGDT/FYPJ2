using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.Hive;
using Photon.Hive.Plugin;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace TestPlugin
{
    public class RaiseEventTestPlugin : PluginBase
    {
        public const byte B_Check_Data = 10;
        public const byte B_Send_Word = 11;

        private string connStr;
        private MySqlConnection conn;

        public string ServerString
        {
            get;
            private set;
        }
        public int CallsCount
        {
            get;
            private set;
        }

        public RaiseEventTestPlugin()
        {
            this.UseStrictMode = true;
            this.ServerString = "ServerMessage";
            this.CallsCount = 0;

            // --- Connect to MySQL.
            ConnectToMySQL();
        }

        public override string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public override void OnRaiseEvent(IRaiseEventCallInfo info)
        {
            try
            {
                base.OnRaiseEvent(info);
            }
            catch (Exception e)
            {
                this.PluginHost.BroadcastErrorInfoEvent(e.ToString(), info);
                return;
            }

            switch (info.Request.EvCode)
            {
                case B_Check_Data:
                    {
                        CheckWord(info);
                    }
                    break;
                case B_Send_Word:
                    {
                        SendWordToOpp(info);
                    }
                    break;
                default:
                    break;
            }
        }

        public void ConnectToMySQL()
        {
            // Connect to MySQL
            connStr = "server=localhost;user=root;database=knovv_it;port=3306;password=Comp@q123asus321";
            conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void DisconnectFromMySQL()
        {
            conn.Close();
        }

        private void CheckWord(IRaiseEventCallInfo info)
        {
            string RecvdMessage = Encoding.Default.GetString((byte[])info.Request.Data);

            string sql = "SELECT word FROM eng_words WHERE word=@answer";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@answer", RecvdMessage);

            using (MySqlDataReader result = cmd.ExecuteReader())
            {
                string ReturnMessage = "";
                if (result.HasRows)
                    ReturnMessage = "success";
                else
                    ReturnMessage = "fail";

                this.PluginHost.BroadcastEvent(target: ReciverGroup.All,
                                                senderActor: 0,
                                                targetGroup: 0,
                                                data: new Dictionary<byte, object>() { { (byte)245, ReturnMessage } },
                                                evCode: info.Request.EvCode,
                                                cacheOp: 0);
            }
        }

        private void SendWordToOpp(IRaiseEventCallInfo info)
        {
            string RecvdMessage = Encoding.Default.GetString((byte[])info.Request.Data);
            string ReturnMessage = info.UserId+"="+RecvdMessage;
            this.PluginHost.BroadcastEvent(target: ReciverGroup.All,
                                                senderActor: 0,
                                                targetGroup: 0,
                                                data: new Dictionary<byte, object>() { { (byte)245, ReturnMessage } },
                                                evCode: info.Request.EvCode,
                                                cacheOp: 0);
        }
    }

}
