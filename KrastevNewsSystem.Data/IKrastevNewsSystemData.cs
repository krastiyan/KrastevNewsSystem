namespace KrastevNewsSystem.Data
{
    using KrastevNewsSystem.Data.Repositories;
    using KrastevNewsSystem.Models;

    public interface IKrastevNewsSystemData
    {
        IRepository<NewsApplicationUser> Users
        {
            get;
        }

        //IRepository<Teacher> Teachers
        //{
        //    get;
        //}

        //IRepository<Student> Students
        //{
        //    get;
        //}

        //IRepository<Group> Groups
        //{
        //    get;
        //}

        //IRepository<Coordinate> Coordinates
        //{
        //    get;
        //}

        //IRepository<Event> Events
        //{
        //    get;
        //}

    }
}
