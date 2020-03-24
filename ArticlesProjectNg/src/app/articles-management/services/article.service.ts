import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable, onErrorResumeNext } from 'rxjs';
import { Article } from '../models/article';
import { Category } from "../models/category";
import { UserComment } from "../models/user-comment";

@Injectable({
  providedIn: 'root'
})
export class ArticleService {
  private articleUrl = environment.apiUrl + 'api/articles/';
  private categoryUrl = environment.apiUrl + 'api/categories/';
  private categoryTypeUrl = environment.apiUrl + 'api/articles/category/';

  constructor(private http: HttpClient) { }

  getArticles(): Observable<Array<Article>> {
    return this.http.get<Array<Article>>(this.articleUrl);
  }

  getCategories(): Observable<Array<Category>> {
    return this.http.get<Array<Category>>(this.categoryUrl);
  }

  getArticle(id: string): Observable<Article> {
    return this.http.get<Article>(`${this.articleUrl}${id}`);
  }

  getCategory(name: string): Observable<Array<Article>> {
    return this.http.get<Array<Article>>(`${this.categoryTypeUrl}${name}`);
  }

  addArticle(customer: Article) : Observable<Article> {
    return this.http.post<Article>(`${this.articleUrl}`, customer);
  }

  addComment(comment: UserComment, id: string) : Observable<UserComment>{
    return this.http.post<UserComment>(`${this.articleUrl}${id}`, comment);
  }
}
