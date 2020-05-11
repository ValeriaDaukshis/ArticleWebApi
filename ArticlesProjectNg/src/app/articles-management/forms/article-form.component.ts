import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ArticleService } from '../services/article.service';
import { Article } from '../models/article';
import { Category } from "../models/category";
import { User } from 'app/registration-management/models/user';
import { UserStorage } from 'app/registration-management/models/userStorage';
import { ArticleBrief } from '../models/articleBrief';


@Component({
  selector: 'app-customer-form',
  templateUrl: './article-form.component.html'
})

export class ArticleFormComponent implements OnInit {

  article = new Article("", "", "", "", "", null, 'business',[]);
  categories: Category[];
  existed = false;
  user: UserStorage;
  

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private customerService: ArticleService,
  ) {
   }

  ngOnInit() {
    this.user = JSON.parse(localStorage.getItem("registrate"));
    this.customerService.getCategories().subscribe(h => this.categories = h);
    this.route.params.subscribe(p => {
      if (p['id'] === undefined) {
        this.existed = false;
        return;
      }
      
      this.customerService.getArticle(p['id']).subscribe(h => this.article = h);
      this.existed = true;
    });
  }

  navigateToCustomers() {
    this.router.navigate(['/cabinet/{{this.article.user.id}}']);
  }

  onCancel() {
    this.navigateToCustomers();
  }
  
  onSubmit(customer: Article) {
    console.log(customer);
    let artBrief: ArticleBrief = this.mapToArticleBrief(customer);
    if(this.existed)
      this.customerService.updateArticle(artBrief.id, artBrief).subscribe(c => this.navigateToCustomers);
    else
      this.customerService.addArticle(artBrief).subscribe(c => this.navigateToCustomers);
  }

  mapToArticleBrief(article: Article): ArticleBrief{
    let artBrief = new ArticleBrief("", "", "", "", "", "", "",[]);
    artBrief.id = article.id;
    artBrief.title = article.title;
    artBrief.categoryName = article.categoryName;
    artBrief.description = article.description;
    artBrief.photo =article.photo;
    artBrief.userId = this.user.id;

    return artBrief;

  }

  handleFileInput(files: FileList) {
    const fileToUpload = files.item(0);
    var reader = new FileReader();
    reader.onload =this.handleFile.bind(this);
    reader.readAsBinaryString(fileToUpload);
  }

  handleFile(event) {
    var binaryString = event.target.result;
    this.article.photo= btoa(binaryString);
   }
}
