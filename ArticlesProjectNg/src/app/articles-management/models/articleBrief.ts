import { User } from "app/registration-management/models/user";
import { Category } from "./category";
import { UserComment } from "./user-comment";
import { SafeUrl } from "@angular/platform-browser";
import { CommentBrief } from "./commentBrief";

export class ArticleBrief {
    constructor(
        public id: string,
        public title: string,
        public photo: any,
        public createdDate: string,
        public description: string,
        public userId: string,
        public categoryName: string,
        public comments: CommentBrief[],
    ) { }
}