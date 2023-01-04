import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'AbpTemplate',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44314/',
    redirectUri: baseUrl,
    clientId: 'AbpTemplate_App',
    responseType: 'code',
    scope: 'offline_access AbpTemplate',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44314',
      rootNamespace: 'AbpTemplate',
    },
  },
} as Environment;
