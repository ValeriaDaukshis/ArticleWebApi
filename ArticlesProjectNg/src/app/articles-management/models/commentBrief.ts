import { User } from "app/registration-management/models/user";

export class CommentBrief {
    constructor(
        public userId: string,
        public commentText: string,
    ){}
}