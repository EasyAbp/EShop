import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'EShop',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44323/',
    redirectUri: baseUrl,
    clientId: 'EShop_App',
    responseType: 'code',
    scope: 'offline_access EShop',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44323',
      rootNamespace: 'EasyAbp.EShop',
    },
  },
} as Environment;
