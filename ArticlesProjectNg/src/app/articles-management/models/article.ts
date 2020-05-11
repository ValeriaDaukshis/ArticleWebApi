import { UserComment } from "./user-comment";
import { UserBrief } from "app/registration-management/models/userBrief";

export class Article {
    constructor(
        public id: string,
        public title: string,
        public photo: any,
        public createdDate: string,
        public description: string,
        public user: UserBrief,
        public categoryName: string,
        public comments: UserComment[],
    ) { }
}