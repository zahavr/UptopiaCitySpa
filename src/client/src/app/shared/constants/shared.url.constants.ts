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
    BuyAppartament: 'api/building/buy-appartaments/:id',
    CreateNewBuilding: 'api/building/add-building'
  },
  Business: {
    GetBusinessApplications: 'api/business/get-business-requests',
    AcceptBusiness: 'api/business/accept-business-request/:businessId',
    CreateBusinessRequest: 'api/business/create-business-request',
    RejectBusiness: 'api/business/reject-business-application',
    GetUserBusiness: 'api/business/get-my-business',
    GetUserApplications: 'api/business/user-business-applications',
    CreateVacancy: 'api/business/create-vacancy',
    GetAllVacancy: 'api/business/get-all-vacancy',
    RespondVacancy: 'api/business/respond-vacancy/:vacancyId',
    GetUserVacancies: 'api/business/get-user-vacancies',
    GetVacanciesRespond: 'api/business/get-vacancies-respond/:businessId',
    AcceptWorker: 'api/business/accept-worker/:vacancyApplicationId',
    GetWorkers: 'api/business/get-workers/:businessId',
    DismissWorker: 'api/business/dismiss-worker/:id',
  },
  Police: {
    GetUsers: 'api/police/get-users',
    GetUser: 'api/police/get-user/:id',
    GetUserBusiness: 'api/police/get-user-business/:id',
    GetUserFriends: 'api/police/get-user-friends/:id',
    GetUserAppartaments: 'api/police/get-user-appartaments/:id',
    GetUserViolations: 'api/police/get-user-violations/:id',
    SetUserViolation: 'api/police/set-violation',
    AmnestyUser: 'api/police/amnesty-user/:id'
  },
  Work: {
    GetUserWork: 'api/work/get-user-work',
    StartShift: 'api/work/start-shift',
    EndShift: 'api/work/close-shift',
    CheckOpenShift: 'api/work/check-open-shift'
  }
};
