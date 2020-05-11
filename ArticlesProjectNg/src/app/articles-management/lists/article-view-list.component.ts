import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ArticleService } from '../services/article.service';
import { Article } from "../models/article";
import { UserComment } from "../models/user-comment";
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { UserStorage } from 'app/registration-management/models/userStorage';
import { CommentBrief } from '../models/commentBrief';
import { UserBrief } from 'app/registration-management/models/userBrief';

@Component({
  //selector: 'app-customer-form',
  templateUrl: './article-view-list.component.html',
  styleUrls: ['./article-view-list.component.css']
})

export class ArticleViewListComponent implements OnInit {

  //article = new Article("", "",null, "", "", null, null, []);
  comment = new UserComment (null, "");
  userComments: UserComment[];
  user: UserStorage;
  article = new Article("", "", null, "", "", new UserBrief("", "", null), "", []);
  existed = false;

  constructor(
      private articleService: ArticleService,
      private router: Router,
      private route: ActivatedRoute,
      private sanitizer: DomSanitizer,
      ) { }

    ngOnInit() {
      var item = localStorage.getItem("registrate");
      if ( item !== null) {
          this.existed = true;
          this.user = JSON.parse(item);
        }

      this.route.params.subscribe(p => {
        this.articleService.getArticle(p['id']).subscribe(h => this.initComponents(h));
      });
    }

    initComponents(h: Article){
        this.article = h;
        this.article.photo = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,' 
                 + this.article.photo);
        this.userComments = this.article.comments;
        this.userComments.forEach(element => {
          element.user.photo = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,' 
          + element.user.photo);
        });
        
    }

    onSubmit(comment: UserComment, articleId: string){
      let commentBrief:CommentBrief  = this.mapToCommentBrief(comment);
      this.articleService.addComment(commentBrief, articleId).subscribe(c => this.SafeUrlPhoto(c));
      comment.commentText = "";
    }

    mapToCommentBrief(userComment: UserComment){
      let comment: CommentBrief = new CommentBrief("", "");
      
      comment.userId = this.user.id;
      comment.commentText = userComment.commentText;
      return comment;

    }

    SafeUrlPhoto(c){
      console.log(c);
      c.user.photo = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,' 
          + c.user.photo);
      this.userComments.push(c);  
    }

    navigateToArticles() {
      this.router.navigate(['/articles']);
    }
  
    onCancel() {
      this.navigateToArticles();
    }
}