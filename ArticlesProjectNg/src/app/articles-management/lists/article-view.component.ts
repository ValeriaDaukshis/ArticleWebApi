import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ArticleService } from '../services/article.service';
import { ViewArticle } from '../models/view-article';
import { User } from "app/registration-management/user";
import { Category } from "../models/category";
import {AutorizationPageComponent} from "app/registration-management/autoriztion-form/autorization-page.component";

@Component({
  selector: 'app-customer-form',
  templateUrl: './article-view-list.component.html',
})

export class ArticleViewListComponent implements OnInit {

  article = new ViewArticle("", "", "", "");
  user: User;
  existed = false;

  constructor(
      private customerService: ArticleService,
      private aurizationPage: AutorizationPageComponent,
      private route: ActivatedRoute,
      ) { }

    ngOnInit() {
        var item = localStorage.getItem("registrate");
      if ( item !== null) {
          this.existed = true;
          this.user = JSON.parse(item);
        }
        this.route.params.subscribe(p => {
            if (p['id'] === undefined) return;
            this.customerService.getCustomer(p['id']).subscribe(h => this.article = h);
          });
    }

    showArticle(articleId: string){
        this.customerService.getCustomer(articleId).subscribe(h => this.article = h);
    }

    setComment(commentText: string){

    }
}