using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serveur.Hubs
{
    public static class MessagesConstants
    {
        public const string CONNECTION_BEGIN = "ConnectionBegin";

        public const string CONNECT = "Connect";

        public const string DISCONNECT = "Disconnect";


        public const string PUBLIC_REMOVED = "PublicRemoved";


        public const string ROOM_CREATED = "RoomCreated";


        public const string NEW_PLAYER = "NewPlayer";


        public const string READY = "Ready";

        public const string ROOM_IS_UNDEFINED = "RoomIsUndefined";

        public const string ROOM_IS_FULFILL = "RoomFulfill";

        public const string ALREADY_IN_ROOM = "AlreadyInRoom";

        public const string NEW_PUBLIC = "NewPublic";

        public const string NOT_IN_THIS_ROOM = "NotInThisRoom";

        public const string PLAYER_REMOVED = "PlayerRemoved";

        public const string ROOM_REMOVED = "RoomRemoved";


        public const string EJECTED_FROM_ROOM = "EjectedFromRoom";
    }
}
