import { UserCommentList } from "./comment-list";
import { User } from "app/registration-management/user";

export class ViewArticle {
    constructor(
        public id: string,
        public title: string,
        public createdDate: string,
        public description: string,
        public commentList: UserCommentList[],
    ) { }
}