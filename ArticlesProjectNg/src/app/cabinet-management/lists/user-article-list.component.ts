import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ArticleService } from 'app/articles-management/services/article.service';
import { Article } from 'app/articles-management/models/article';
import { User } from "app/registration-management/models/user";
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import {AutorizationPageComponent} from "app/registration-management/autoriztion-form/autorization-page.component";
import { element } from 'protractor';
import { UserStorage } from 'app/registration-management/models/userStorage';

@Component({
  selector: 'app-customer-form',
  templateUrl: './user-article-list.component.html',
  styleUrls: ['./user-article-list.component.css']
})

export class UserArticleListComponent implements OnInit {

  userArticles : Article[];
  user: UserStorage;
  existed = false;

  constructor(
      private articleService: ArticleService,
      private sanitizer: DomSanitizer,
      private route: ActivatedRoute,
      private router: Router,
      ) { }

    ngOnInit() {
      var item = localStorage.getItem("registrate");
      this.user = JSON.parse(item);
      this.showAll();
    }

    showAll(){
        this.articleService.getArticlesByUserId(this.user.id).subscribe(h => this.getFile(h));
    }

    navigateToCustomers() {
      this.router.navigate(['/cabinet/{{this.article.user.id}}']);
    }

    getFile(art: Article[]){
      this.userArticles = art;
      this.userArticles.forEach(element =>{
        element.photo = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,' 
        + element.photo);

      });
    }
}