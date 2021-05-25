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
    Detail: 'main/:id',
    OwnAppartaments: 'appartaments'
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
    UserDetail: 'users/:id',
    UserViolation: 'user-violation'
  },
  Work: {
    UserWork: 'user-work'
  }
};
