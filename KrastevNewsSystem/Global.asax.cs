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
                        opt => opt.ResolveUsing<KeywordValidCustomResolver>())
                    //.ReverseMap()
                ;

            });
        }
    }

    class KeywordValidCustomResolver : IValueResolver<ArticleKeyword, ArticleKeywordViewModel, bool>
    {
        public bool Resolve(ArticleKeyword source, ArticleKeywordViewModel destination, bool member, ResolutionContext context)
        {
            DateTime currentDate = DateTime.Now;
            return (source.ValidFrom < currentDate &&
                (source.ValidTo == null || source.ValidTo > currentDate));
        }
    }

}
