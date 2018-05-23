using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Serveur.Models;
using Serveur.Models.BatailleModels;
using Serveur.Models.Exceptions;

namespace Serveur.Hubs
{
    /// <summary>
    /// Realize the connection between the Server and Clients
    /// </summary>
    public class CardGameHub : Hub /*, IContractCardGame, IContractCardGameHub*/
    {

        public static Lazy<APICardGame> cardGame = new Lazy<APICardGame>();
        
        /// <summary>
        /// Appelé lorsqu'un nouvel utilisateur se connecte au serveur.
        /// Envoie l'instance de ce nouvel utilisateur aux autre joueurs et envoie l'ensemble des données du serveur au nouvel utilisateur.
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            ApplicationUser user = cardGame.Value.Connection(Context.ConnectionId);
            await Clients.Caller.SendAsync(MessagesConstants.CONNECTION_BEGIN, user, cardGame.Value.GetUsers(), cardGame.Value.GetRooms());
            await Clients.Others.SendAsync(MessagesConstants.CONNECT, user);

            await base.OnConnectedAsync();
        }

        /// <summary>
        /// Appelé lorsqu'un utilisateur se déconnecte.
        /// Met à jour les données du serveurs en supprimant ce qui doit l'être,
        /// prévient les autre utilisateurs de la déconnexion et des midifcations.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception ex)
        {
            try
            {
                List<string> roomsToUpdate = cardGame.Value.RemovingUser(Context.ConnectionId);
                foreach(string roomId in roomsToUpdate)
                {
                    await UpdateRoom(roomId, Context.ConnectionId);
                }
                await Clients.All.SendAsync(MessagesConstants.DISCONNECT, Context.ConnectionId);
            }
            catch (UserIsUndefinedException)
            {

            }
        }

        /// <summary>
        /// Met à jour l'état d'une Room suite à la déconnexion d'un ApplicationUser et prévient les autre utilisateurs.
        /// </summary>
        /// <param name="roomId">Id de la room concernée</param>
        /// <param name="userId">Id de l'Applicationuser concerné</param>
        /// <returns></returns>
        public async Task UpdateRoom(string roomId, string userId)
        {
            bool toDestroy = cardGame.Value.UpdateRoom(roomId, userId);
            if (toDestroy)
            {
                RemovingRoom(roomId);
            }
            else
            {
                await Clients.All.SendAsync(MessagesConstants.PUBLIC_REMOVED, roomId, userId);
            }
        }

        /// <summary>
        /// Appelée suite à une demande de création de Room. Crée la nouvelle Room et prévient les utilisateurs
        /// </summary>
        /// <returns></returns>
        public async Task CreatingRoom()
        {
            Room room = cardGame.Value.CreatingRoom();
            await Clients.All.SendAsync(MessagesConstants.ROOM_CREATED, room);
            AddingPlayer(room.RoomId);
        }

        /// <summary>
        /// Ajoute l'utilisateur ayant fait la demande dans la liste des joueurs d'une Room et prévient les Utilisateurs.
        /// Vérifie également si la Partie est prête à commencer et prévient les Utilisateurs.
        /// </summary>
        /// <param name="roomId">Id de la Room concernée</param>
        /// <returns></returns>
        public async Task AddingPlayer(string roomId)
        {
            try
            {
                bool ready = cardGame.Value.AddingPlayer(roomId, Context.ConnectionId);
                await Clients.All.SendAsync(MessagesConstants.NEW_PLAYER, roomId, Context.ConnectionId);
                if (ready)
                {
                    await Clients.All.SendAsync(MessagesConstants.READY, roomId);
                    BatailleBegin(roomId);
                    
                }
            }
            catch (UserIsUndefinedException)
            {
                
            }
            catch (RoomIsUndefinedException)
            {
                await Clients.Caller.SendAsync(MessagesConstants.ROOM_IS_UNDEFINED, roomId);
            }
            catch (FulfillRoomException)
            {
                await Clients.Caller.SendAsync(MessagesConstants.ROOM_IS_FULFILL, roomId);
            }
            catch (AlreadyInRoomException)
            {
                await Clients.Caller.SendAsync(MessagesConstants.ALREADY_IN_ROOM, roomId);
            }
        }

        /// <summary>
        /// Ajoute l'utilisateur ayant fait la demande dans le public d'une Room et prévient les utilisateurs.
        /// </summary>
        /// <param name="roomId">Id de la room concernée.</param>
        /// <returns></returns>
        public async Task AddingPublic(string roomId)
        {
            try
            {
                cardGame.Value.AddingPublic(roomId, Context.ConnectionId);
                await Clients.All.SendAsync(MessagesConstants.NEW_PUBLIC, roomId, Context.ConnectionId);
            }
            catch (UserIsUndefinedException)
            {

            }
            catch (RoomIsUndefinedException)
            {
                await Clients.Caller.SendAsync(MessagesConstants.ROOM_IS_UNDEFINED, roomId);
            }
            catch (AlreadyInRoomException)
            {
                await Clients.Caller.SendAsync(MessagesConstants.ALREADY_IN_ROOM, roomId);
            }
        }

        /// <summary>
        /// Supprime l'utilisateur ayant fait la demande du public d'une Room et prévient les utilisateurs.
        /// </summary>
        /// <param name="roomId">Id de la Rooom concecrnée</param>
        /// <returns></returns>
        public async Task RemovingPublic(string roomId)
        {
            try
            {
                cardGame.Value.RemovingPublic(roomId, Context.ConnectionId);
                await Clients.All.SendAsync(MessagesConstants.PUBLIC_REMOVED, roomId, Context.ConnectionId);
            }
            catch (UserIsUndefinedException)
            {

            }
            catch (RoomIsUndefinedException)
            {
                await Clients.Caller.SendAsync(MessagesConstants.ROOM_IS_UNDEFINED, roomId);
            }
            catch (NotInThisRoomException)
            {
                await Clients.Caller.SendAsync(MessagesConstants.NOT_IN_THIS_ROOM, roomId);
            }
        }

        /// <summary>
        /// Supprime l'Utilisateur ayant fait la demande de la liste des joueurs d'un Room
        /// et prévient les utilisteurs et applique les modification nécéssaires (si besoin).
        /// </summary>
        /// <param name="roomId">Id de la Room concernée</param>
        /// <returns></returns>
        public async Task RemovingPlayer(string roomId)
        {
            try
            {
                bool toDestroy = cardGame.Value.RemovingPlayer(roomId, Context.ConnectionId);
                await Clients.All.SendAsync(MessagesConstants.PLAYER_REMOVED, roomId, Context.ConnectionId);
                if (toDestroy)
                {
                    RemovingRoom(roomId);
                }
            }
            catch (UserIsUndefinedException)
            {

            }
            catch (RoomIsUndefinedException)
            {
                await Clients.Caller.SendAsync(MessagesConstants.ROOM_IS_UNDEFINED, roomId);
            }
            catch (NotInThisRoomException)
            {
                await Clients.Caller.SendAsync(MessagesConstants.NOT_IN_THIS_ROOM, roomId);
            }

        }

        /// <summary>
        /// Supprime une Room du serveur (suite à une partie annulé par le départ d'un joueur) et prévient les utilisateurs.
        /// </summary>
        /// <param name="roomId">Id de la Room concernée</param>
        /// <returns></returns>
        public async Task RemovingRoom(string roomId)
        {
            try
            {
                List<string> usersToExtract = cardGame.Value.RemovingRoom(roomId);
                await ExtractingUsers(usersToExtract, roomId);
                await Clients.All.SendAsync(MessagesConstants.ROOM_REMOVED, roomId);
            }
            catch (RoomIsUndefinedException)
            {

            }
        }

        /// <summary>
        /// Prévient l'ensemle des joueurs ayant été dans une Room ayant été supprimée de leur expulsion de la room. 
        /// </summary>
        /// <param name="usersToExtract">Liste des identifiants des joueurs concernés</param>
        /// <param name="roomId">Id de la Room concernée</param>
        /// <returns></returns>
        public async Task ExtractingUsers(List<string> usersToExtract, string roomId)
        {
            foreach(string userId in usersToExtract)
            {
                await Clients.Client(userId).SendAsync(MessagesConstants.EJECTED_FROM_ROOM, roomId);
            }

        }

        public async Task BatailleBegin(string roomId)
        {
            List<Player> players = cardGame.Value.BatailleBegin(roomId);
            foreach(Player p in players)
            {
                await Clients.Client(p.UserId).SendAsync("Begin", roomId, p.Hand);
            }
        }

        public async Task CardPlayed(string roomId, int cardIndex)
        {
            Console.WriteLine("Received");
            bool ready = cardGame.Value.CardPlayed(roomId, Context.ConnectionId, cardIndex);
            await Clients.Caller.SendAsync("ConfirmCard", roomId, cardIndex);
            if (ready)
            {
                FinalizeTour(roomId);
            }
        }

        public async Task FinalizeTour(string roomId)
        {
            

        }
    }
}
