import { User } from "app/registration-management/models/user";
import { UserBrief } from "app/registration-management/models/userBrief";

export class UserComment {
    constructor(
        public user: UserBrief,
        public commentText: string,
    ){}
}