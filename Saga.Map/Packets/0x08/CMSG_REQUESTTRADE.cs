using Saga.Network.Packets;
using System;

namespace Saga.Packets
{
    /// <summary>
    ///
    /// </summary>
    /// <remarks>
    /// This packet is sent by a player when he/she clicks on a local user
    /// and presses the "Request to trade" button. Basicly meaning local
    /// trading invitation.
    /// </remarks>
    /// <id>
    /// 0801
    /// </id>
    internal class CMSG_REQUESTTRADE : RelayPacket
    {
        public CMSG_REQUESTTRADE()
        {
            this.data = new byte[0];
        }

        public uint TargetActor
        {
            get { return BitConverter.ToUInt32(this.data, 0); }
        }

        #region Conversions

        public static explicit operator CMSG_REQUESTTRADE(byte[] p)
        {
            /*
            // Creates a new byte with the length of data
            // plus 4. The first size bytes are used like
            // [PacketSize][PacketId][PacketBody]
            //
            // Where Packet Size equals the length of the
            // Packet body, Packet Identifier, Packet Size
            // Container.
            */

            CMSG_REQUESTTRADE pkt = new CMSG_REQUESTTRADE();
            pkt.data = new byte[p.Length - 14];
            pkt.session = BitConverter.ToUInt32(p, 2);
            Array.Copy(p, 6, pkt.cmd, 0, 2);
            Array.Copy(p, 12, pkt.id, 0, 2);
            Array.Copy(p, 14, pkt.data, 0, p.Length - 14);
            return pkt;
        }

        #endregion Conversions
    }
}