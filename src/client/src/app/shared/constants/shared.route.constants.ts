export const AppRoute = {
  Account: {
    Login: 'login',
    Register: 'register'
  },
  PersonalCabinet: {
    Main: 'main',
    Friend: 'list'
  },
  Building: {
    Main: 'main',
    Detail: ':id'
  },
  CityManagment: {
    BusinessApplications: 'business-applications'
  },
  Business: {
    Main: 'main',
    RequestList: 'requests-list',
    VacancyList: 'vacancy-list'
  },
  Police: {
    Users: 'users',
    UserDetail: ':id'
  },
  Work: {
    UserWork: 'user-work'
  }
};
