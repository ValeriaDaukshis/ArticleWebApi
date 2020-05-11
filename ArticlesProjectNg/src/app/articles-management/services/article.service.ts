import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable, onErrorResumeNext } from 'rxjs';
import { Article } from '../models/article';
import { Category } from "../models/category";
import { UserComment } from "../models/user-comment";
import { ArticleBrief } from '../models/articleBrief';
import { CommentBrief } from '../models/commentBrief';

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

  getArticlesByUserId(id: string): Observable<Array<Article>> {
    return this.http.get<Array<Article>>(`${this.articleUrl}${id}/cabinet`);
  }

  getCategory(name: string): Observable<Array<Article>> {
    return this.http.get<Array<Article>>(`${this.categoryTypeUrl}${name}`);
  }

  addArticle(customer: ArticleBrief) : Observable<Object> {
    return this.http.post<ArticleBrief>(`${this.articleUrl}`, customer);
  }

  addComment(comment: CommentBrief, id: string) : Observable<Object>{
    return this.http.post<CommentBrief>(`${this.articleUrl}${id}`, comment);
  }

  deleteArticle(id: string) : Observable<Object>{
    return this.http.delete<Object>(`${this.articleUrl}${id}`);
  }

  updateArticle(id: string, article: ArticleBrief): Observable<Object>{
    return this.http.put<ArticleBrief>(`${this.articleUrl}${id}`, article);
  }
}
