import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainPageComponent } from 'app/main-page/main-page.component';
import { RegistrationPageComponent } from 'app/registration-management/registration-form/registration-page.component';
import { AutorizationPageComponent } from 'app/registration-management/autoriztion-form/autorization-page.component';
import { ArticleListComponent } from 'app/articles-management/lists/article-list.component';
import { ArticleFormComponent } from 'app/articles-management/forms/article-form.component';
import { ArticleViewListComponent } from 'app/articles-management/lists/article-view.component';


const routes: Routes = [
  { path: '', redirectTo: '/main', pathMatch: 'full' },
  { path: 'main', component: MainPageComponent },
  { path: 'registration', component: RegistrationPageComponent },
  { path: 'autorization', component: AutorizationPageComponent },
  { path: 'articles', component: ArticleListComponent },
  { path: 'articles/category/:categoryName', component: ArticleListComponent },
  { path: 'articles/create', component: ArticleFormComponent },
  { path: 'articles/:id', component: ArticleViewListComponent },
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
