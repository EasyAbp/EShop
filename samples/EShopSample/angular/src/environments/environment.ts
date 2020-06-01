export const environment = {
  production: false,
  application: {
    name: 'EShopSample',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44340',
    clientId: 'EShopSample_App',
    dummyClientSecret: '1q2w3e*',
    scope: 'EShopSample',
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
    defaultResourceName: 'EShopSample',
  },
};
