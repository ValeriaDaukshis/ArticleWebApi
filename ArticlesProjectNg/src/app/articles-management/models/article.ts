import { User } from "app/registration-management/models/user";
import { Category } from "./category";
import { UserComment } from "./user-comment";

export class Article {
    constructor(
        public id: string,
        public title: string,
        public createdDate: string,
        public description: string,
        public user: User,
        public category: Category,
        public comments: UserComment[],
    ) { }
}