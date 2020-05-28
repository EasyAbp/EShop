export const environment = {
  production: true,
  application: {
    name: 'EasyMall',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44340',
    clientId: 'EasyMall_App',
    dummyClientSecret: '1q2w3e*',
    scope: 'EasyMall',
    showDebugInformation: true,
    oidc: false,
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44340',
    },
  },
  localization: {
    defaultResourceName: 'EasyMall',
  },
};
