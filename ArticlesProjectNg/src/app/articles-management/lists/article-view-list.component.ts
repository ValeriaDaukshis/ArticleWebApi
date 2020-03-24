import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ArticleService } from '../services/article.service';
import { ViewArticle } from '../models/view-article';
import { User } from "app/registration-management/models/user";
import { Article } from "../models/article";
import { UserComment } from "../models/user-comment";
import {AutorizationPageComponent} from "app/registration-management/autoriztion-form/autorization-page.component";

@Component({
  //selector: 'app-customer-form',
  templateUrl: './article-view-list.component.html',
  styleUrls: ['./article-list.component.css']
})

export class ArticleViewListComponent implements OnInit {

  article = new Article("", "", "", "", null, null, []);
  comment = new UserComment (null, "");
  commentList: UserComment[];
  user: User;
  existed = false;

  constructor(
      private articleService: ArticleService,
      private router: Router,
      private route: ActivatedRoute,
      ) { }

    ngOnInit() {
      this.route.params.subscribe(p => {
        if (p['id'] === undefined) return;
        this.articleService.getArticle(p['id']).subscribe(h => this.initComponents(h));
      });

      var item = localStorage.getItem("registrate");
      if ( item !== null) {
          this.existed = true;
          this.user = JSON.parse(item);
        }
    }

    initComponents(h: Article){
        this.article = h;
        this.commentList = this.article.comments;
    }

    onSubmit(comment: UserComment, articleId: string){
        comment.userName = this.user.name;
        this.articleService.addComment(this.comment, articleId).subscribe(c => this.commentList.push(c));
    }

    navigateToArticles() {
      this.router.navigate(['/articles']);
    }
  
    onCancel() {
      this.navigateToArticles();
    }
}