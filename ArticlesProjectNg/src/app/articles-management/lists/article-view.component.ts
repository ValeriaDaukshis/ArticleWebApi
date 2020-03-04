import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ArticleService } from '../services/article.service';
import { ViewArticle } from '../models/view-article';
import { User } from "app/registration-management/user";
import { Article } from "../models/article";
import { UserComment } from "../models/comment";
import { UserCommentList } from "../models/comment-list";
import {AutorizationPageComponent} from "app/registration-management/autoriztion-form/autorization-page.component";

@Component({
  selector: 'app-customer-form',
  templateUrl: './article-view-list.component.html',
  styleUrls: ['./article-list.component.css']
})

export class ArticleViewListComponent implements OnInit {

  article : Article;
  comments = new UserComment ("", "");
  commentList: UserCommentList[];
  user: User;
  existed = false;

  constructor(
      private customerService: ArticleService,
      private aurizationPage: AutorizationPageComponent,
      private route: ActivatedRoute,
      ) { }

    ngOnInit() {
      this.route.params.subscribe(p => {
        if (p['id'] === undefined) return;
        this.customerService.getArticle(p['id']).subscribe(h => this.article = h);
        console.log(this.article);
      });

      var item = localStorage.getItem("registrate");
      if ( item !== null) {
          this.existed = true;
          this.user = JSON.parse(item);
        }
        
    }

    onSubmit(comment: UserComment, articleId: string){
        comment.userName = this.user.name;
        this.customerService.addComment(this.comments, articleId).subscribe();
    }
}