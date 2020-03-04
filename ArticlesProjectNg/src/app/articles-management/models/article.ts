import { User } from "app/registration-management/user";
import { Category } from "./category";
import { UserCommentList } from "./comment-list";

export class Article {
    constructor(
        public id: string,
        public title: string,
        public createdDate: string,
        public description: string,
        public user: User,
        public category: Category,
        public commentList: UserCommentList[],
    ) { }
}