import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ArticleService } from '../services/article.service';
import { Article } from '../models/article';
import { Category } from "../models/category";


@Component({
  selector: 'app-customer-form',
  templateUrl: './article-form.component.html'
})

export class ArticleFormComponent implements OnInit {

  article = new Article("", "", "", "", null, new Category("", ""));
  categories: Category[];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private customerService: ArticleService,
  ) {
   }

  ngOnInit() {
      this.article.user = JSON.parse(localStorage.getItem("registrate"));
    this.customerService.getCategories().subscribe(h => this.categories = h);
  }

  navigateToCustomers() {
    this.router.navigate(['/articles']);
  }

  onCancel() {
    this.navigateToCustomers();
  }
  
  onSubmit(customer: Article) {
    this.customerService.addCustomer(customer).subscribe(c => this.navigateToCustomers);
  }
}
