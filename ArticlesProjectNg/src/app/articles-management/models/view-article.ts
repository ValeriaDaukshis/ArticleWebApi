import { UserComment } from "./user-comment";
import { User } from "app/registration-management/models/user";

export class ViewArticle {
    constructor(
        public id: object,
        public title: string,
        public createdDate: string,
        public description: string,
        public commentList: UserComment[],
    ) { }
}