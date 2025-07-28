namespace Minesweeper.Domain.Common
{
    public abstract class CreatableEntity : BaseEntity
    {
        public DateTime CreatedAt { get; set; }
    }
}
