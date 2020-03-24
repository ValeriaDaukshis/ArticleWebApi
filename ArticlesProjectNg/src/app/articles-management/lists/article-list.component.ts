import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ArticleService } from '../services/article.service';
import { Article } from '../models/article';
import { User } from "app/registration-management/models/user";
import { Category } from "../models/category";
import {AutorizationPageComponent} from "app/registration-management/autoriztion-form/autorization-page.component";

@Component({
  selector: 'app-customer-form',
  templateUrl: './article-list.component.html',
  styleUrls: ['./article-list.component.css']
})

export class ArticleListComponent implements OnInit {

  articles : Article[];
  categories: Category[];
  user: User;
  existed = false;

  constructor(
      private articleService: ArticleService,
      private aurizationPage: AutorizationPageComponent,
      ) { }

    ngOnInit() {
      var item = localStorage.getItem("registrate");
      if ( item !== null) {
          this.existed = true;
          this.user = JSON.parse(item);
        }
      this.showAll();
    }

    showAll(){
        this.articleService.getArticles().subscribe(h => this.articles = h);
        this.articleService.getCategories().subscribe(h => this.categories = h);
    }

    getCategory(name: string){
        this.articleService.getCategory(name).subscribe(h => this.articles = h);
    }
}