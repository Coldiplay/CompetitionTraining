using API.DB;

namespace API
{
    public class DBExample
    {
        private static CompetitionContext db;
        public static CompetitionContext GetDB() => db ??= new CompetitionContext();
    }
}
