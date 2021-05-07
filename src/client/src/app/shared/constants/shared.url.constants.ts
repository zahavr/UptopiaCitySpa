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
    UpdateProfileInfo: 'api/user/update-user'
  },
  Building: {
    GetAppartaments: 'api/building/get-appartaments',
    GetAppartament: 'api/building/get-appartament/:id',
    GetRandomAppartaments: 'api/building/get-random-appartaments',
    BuyAppartament: 'api/building/buy-appartaments/:id'
  }
};
