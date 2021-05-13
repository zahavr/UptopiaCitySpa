export const ApiUrl = {
  Account: {
    Register: 'api/account/register',
    Login: 'api/account/login',
    CurrentUser: 'api/account/current-user',
    EmailExists: 'api/account/emailexists?email=:email',
  },
  Errors: {
    NotFound: 'api/buggy/notfound',
    UnAuth: 'api/buggy/testauth',
    ServerError: 'api/buggy/servererror',
    BadRequest: 'api/buggy/badrequest',
    ValidationError: 'api/buggy/badrequest/fortytwo'
  },
  User: {
    Info: 'api/user/user-info',
    UploadPhoto: 'api/user/upload-photo',
    UpdateProfileInfo: 'api/user/update-user',
    GetFriendList: 'api/user/list-friends',
    FindFriend: 'api/user/find-friend/:email',
    AcceptFriendRequest: 'api/user/accept-friend-request/:id',
    RejectFriendRequest: 'api/user/reject-friend-request/:id',
    DeleteFriend: 'api/user/delete-friend/:id',
    CreateFriendRequest: 'api/user/create-friend-request',
    GetFriendsRequest: 'api/user/list-request-friends'
  },
  Building: {
    GetAppartaments: 'api/building/get-appartaments',
    GetAppartament: 'api/building/get-appartament/:id',
    GetRandomAppartaments: 'api/building/get-random-appartaments',
    BuyAppartament: 'api/building/buy-appartaments/:id'
  },
  Business: {
    GetBusinessApplications: 'api/business/get-business-requests',
    AcceptBusiness: 'api/business/accept-business-request/:businessId',
    RejectBusiness: 'api/business/reject-business-application',
    GetUserBusiness: 'api/business/get-my-business',
    GetUserApplications: 'api/business/user-business-applications'
  }
};
