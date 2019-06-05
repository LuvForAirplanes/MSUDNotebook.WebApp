namespace SolidMapper
{
    public interface IMapper<TDTO, TEntity>
    {
        TEntity Map(TDTO source);

        TDTO Map(TEntity source);

        void Map(TEntity source, TDTO target);

        void Map(TDTO source, TEntity target);
    }
}