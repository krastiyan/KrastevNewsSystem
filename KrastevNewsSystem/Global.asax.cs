using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using KrastevNewsSystem.Models;
using KrastevNewsSystem.Controllers;
using KrastevNewsSystem.Data;

namespace KrastevNewsSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Configuring mappings between Data and View models
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<NewsArticle, NewsArticleViewModel>()
                    .ForMember(dest => dest.ArticleID,
                        opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.ArticleAuthor,
                        opt => opt.MapFrom(src => src.ArticleAuthor.UserName))
                 ;
                cfg.CreateMap<NewsArticleComment, NewsArticleCommentViewModel>()
                    .ForMember(dest => dest.CommentAuthor,
                        opt => opt.MapFrom(src => src.CommentAuthor.UserName))
                    .ForMember(dest => dest.CommentedNewsArticleID,
                        opt => opt.MapFrom(src => src.CommentedNewsArticle.Id))
                    .ForMember(dest => dest.CommentRepliedToID,
                        opt => opt.MapFrom(src => src.CommentRepliedTo.Id))
                            .ForMember(dest => dest.CommentRepliedToID,
                            opt => opt.AllowNull())
                    .ForMember(dest => dest.CommentRepliedToAuthor,
                        opt => opt.MapFrom(src => src.CommentRepliedTo.CommentAuthor.UserName))
                            .ForMember(dest => dest.CommentRepliedToAuthor,
                            opt => opt.AllowNull())
                    .ForMember(dest => dest.CommentRepliedToDate,
                        opt => opt.MapFrom(src => src.CommentRepliedTo.PostedOn))
                            .ForMember(dest => dest.CommentRepliedToDate,
                            opt => opt.AllowNull())
                   ;

                cfg.CreateMap<NewsArticleCommentViewModel, NewsArticleComment>()
                    .ForMember(dest => dest.CommentAuthor,
                        opt => opt.ResolveUsing<UserFromUserNameCustomResolver, string>(src => src.CommentAuthor)
                        )
                    .ForMember(dest => dest.CommentedNewsArticle,
                        opt => opt.ResolveUsing<ArticleFromIDCustomResolver, int>(src => src.CommentedNewsArticleID))
                    .ForMember(dest => dest.CommentRepliedTo,
                        opt => opt.ResolveUsing<CommentFromIDCustomResolver, int>(src => src.CommentRepliedToID))
                            .ForMember(dest => dest.CommentRepliedTo,
                            opt => opt.AllowNull())
                    .ForMember(dest => dest.PostedOn,
                        opt => opt.AllowNull())
                   ;

                cfg.CreateMap<ArticleKeywordViewModel, ArticleKeyword>()
                    .ForMember(dest => dest.KeywordedArticles,
                        opt => opt.AllowNull())
                    .ForMember(dest => dest.ValidTo,
                        opt => opt.AllowNull())
                    .ForSourceMember(src => src.IsKeywordValid,
                        opt => opt.Ignore())
                ;
                cfg.CreateMap<ArticleKeyword, ArticleKeywordViewModel>()
                    .ForMember(dest => dest.KeywordedArticles,
                        opt => opt.AllowNull())
                    .ForMember(dest => dest.ValidTo,
                        opt => opt.AllowNull())
                    .ForMember(dest => dest.IsKeywordValid,
                        opt => opt.ResolveUsing<IsKeywordValidCustomResolver>())
                ;

            });
        }
    }

    class IsKeywordValidCustomResolver : IValueResolver<ArticleKeyword, ArticleKeywordViewModel, bool>
    {
        public bool Resolve(ArticleKeyword source, ArticleKeywordViewModel destination, bool member, ResolutionContext context)
        {
            DateTime currentDate = DateTime.Now;
            return (source.ValidFrom < currentDate &&
                (source.ValidTo == null || source.ValidTo > currentDate));
        }
    }

  //Should a custom resolver be used or set DatecCommented to Now in logic is more correct?
    //class UserFromUserNameCustomResolver : IMemberValueResolver<object, object, DateTime, DateTime>
    //{
    //    public DateTime Resolve(object source, object destination, DateTime sourceMember, DateTime destinationMember, ResolutionContext context)
    //    {
    //        // logic here
    //        return DateTime.Now;
    //    }
    //}

    class UserFromUserNameCustomResolver : BaseController
        , IMemberValueResolver<object, object, string, NewsApplicationUser>
    {
        public UserFromUserNameCustomResolver():base(new KrastevNewsSystemDataPersister())
        {

        }
        public NewsApplicationUser Resolve(object source, object destination, string sourceMember, NewsApplicationUser destinationMember, ResolutionContext context)
        {
            // logic here
            return this.DataManager.Users.All().FirstOrDefault(u => u.UserName == sourceMember);
        }
    }

    class ArticleFromIDCustomResolver : BaseController
        , IMemberValueResolver<object, object, int, NewsArticle>
    {
        public ArticleFromIDCustomResolver() : base(new KrastevNewsSystemDataPersister())
        {

        }
        public NewsArticle Resolve(object source, object destination, int sourceMember, NewsArticle destinationMember, ResolutionContext context)
        {
            // logic here
            return this.DataManager.Articles.All().FirstOrDefault(p => p.Id == sourceMember);
        }
    }

    class CommentFromIDCustomResolver : BaseController
        , IMemberValueResolver<object, object, int, NewsArticleComment>
    {
        public CommentFromIDCustomResolver() : base(new KrastevNewsSystemDataPersister())
        {

        }
        public NewsArticleComment Resolve(object source, object destination, int sourceMember, NewsArticleComment destinationMember, ResolutionContext context)
        {
            // logic here
            return this.DataManager.ArticlesComments.All().FirstOrDefault(c => c.Id == sourceMember);
        }
    }

}
