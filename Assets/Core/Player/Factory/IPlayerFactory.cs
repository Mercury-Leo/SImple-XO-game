using Core.Player.Scripts;

namespace Core.Player.Factory {
    public interface IPlayerFactory<T> where T : IPlayer 
    {
        public abstract T CreatePlayer();
    }
}
