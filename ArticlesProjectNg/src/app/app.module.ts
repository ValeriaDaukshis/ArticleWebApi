import { HttpClient, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppRoutingModule } from 'app/app-routing.module';
import { AppComponent } from 'app/app.component';

import { RegistrationPageComponent } from 'app/registration-management/registration-form/registration-page.component';
import { AutorizationPageComponent } from 'app/registration-management/autoriztion-form/autorization-page.component';
import { ArticleListComponent } from 'app/articles-management/lists/article-list.component';
import { ArticleFormComponent } from 'app/articles-management/forms/article-form.component';
import { ArticleViewListComponent } from 'app/articles-management/lists/article-view-list.component';
import { UserArticleListComponent } from 'app/cabinet-management/lists/user-article-list.component';

import { UserService } from 'app/registration-management/services/user.service';
import { ArticleService } from 'app/articles-management/services/article.service';

@NgModule({
  declarations: [
    AppComponent,
    RegistrationPageComponent,
    AutorizationPageComponent,
    ArticleListComponent,
    ArticleFormComponent,
    ArticleViewListComponent,
    UserArticleListComponent,

  ],
  imports: [
    BrowserModule,
    FormsModule,
    NgbModule.forRoot(),
    HttpClientModule,
    AppRoutingModule,
  ],
  providers: [
    HttpClient,
    UserService,
    ArticleService,
    AutorizationPageComponent,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
