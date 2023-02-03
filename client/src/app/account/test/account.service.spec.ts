import { AccountService } from '../account.service';
import {
  HttpClientTestingModule,
  HttpTestingController,
} from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { Store, MemoizedSelector } from '@ngrx/store';
import { provideMockStore, MockStore } from '@ngrx/store/testing';
import { IUser } from '../../shared/models/user';
import { environment } from 'src/environments/environment';
import * as accountStore from 'src/app/state/account/account.reducer';
import * as accountSelector from 'src/app/state/account/account.selectors';
import {
  getValidUserLoginData,
  getValidUserLoginResponseData,
  getInvalidUserLoginData,
  getInvalidUserLoginResponseData,
} from 'src/app/account/test/account.test-setup-data';

// Account service test suite
describe('Account Service Test', () => {
  let accountService: AccountService;
  let httpTestingController: HttpTestingController;
  let mockStore: MockStore<accountStore.IAccountState>;
  let mockLocalstore = {};
  const initialState = {
    accState: {
      user: null,
      status: '',
      error: '',
      apiErrorResponse: null,
    },
  };
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [AccountService, provideMockStore({ initialState })],
    });
    accountService = TestBed.inject(AccountService);
    httpTestingController = TestBed.inject(HttpTestingController);

    // Mock for Ng-Rx Store and Selector
    mockStore = TestBed.get(Store);
    let mockAccountTokenSelector: MemoizedSelector<
      accountStore.IAccountState,
      string
    >;
    mockAccountTokenSelector = mockStore.overrideSelector(
      accountSelector.accountTokenSelector,
      'AA'
    );
  });

  it('Should pass login with valid user details', () => {
    accountService
      .login(getValidUserLoginData())
      .subscribe((response: IUser) => {
        expect(response).toBeTruthy();
        expect(response).withContext('Invalid User');
        expect(response.email).toBe('subash.barik@gmail.com');
        expect(response.isAdmin).toBe(true);
        expect(response.apiErrorResponse).toBe(null);
      });
    const req = httpTestingController.expectOne(
      environment.apiUrl + 'account/login'
    );
    expect(req.request.method).toEqual('POST');
    req.flush(getValidUserLoginResponseData());
  });

  it('Should fail login with in-valid user details', () => {
    accountService
      .login(getInvalidUserLoginData())
      .subscribe((response: IUser) => {
        expect(response).toBeTruthy();
        expect(response.email).toBe(null);
        expect(response.isAdmin).toBe(null);
        expect(response.displayName).toBe(null);
        expect(response.token).toBe(null);
        expect(response.roles).toBe(null);
        expect(response.apiErrorResponse).toBeTruthy();
        expect(response.apiErrorResponse.statusCode).toBe('401');
        expect(response.apiErrorResponse.errors[0]).toBe(
          'Unable to find User.'
        );
        expect(response.apiErrorResponse.message).toBe(
          'You are not authorized'
        );
      });
    const req = httpTestingController.expectOne(
      environment.apiUrl + 'account/login'
    );
    expect(req.request.method).toEqual('POST');
    req.flush(getInvalidUserLoginResponseData());
  });

  it('Should register a user with valid user details', () => {
    // will be similar to login user with valid data
    pending();
  });

  it('Should fail register with in-valid user details', () => {
    // will be similar to login user with in-valid data
    pending();
  });

  it('Should get token for user from account selector', () => {
    mockStore.refreshState();
    let token = accountService.getJwtToken();
    expect(token).toBe('AA');
  });

  it('Should get token for user from localStorage if present', () => {
    spyOn(localStorage, 'getItem').and.callFake((key: string): string => {
      return mockLocalstore[key] || null;
    });
    spyOn(localStorage, 'setItem').and.callFake(
      (key: string, value: string): string => {
        return (mockLocalstore[key] = <string>value);
      }
    );
    localStorage.setItem('token', 'AAA');
    mockStore.refreshState();
    let token = accountService.getJwtToken();
    expect(localStorage.getItem('token')).toBe('AAA');
  });
  it('Should clear token when user logs out', () => {
    spyOn(localStorage, 'getItem').and.callFake((key: string): string => {
      return mockLocalstore[key] || null;
    });
    spyOn(localStorage, 'setItem').and.callFake(
      (key: string, value: string): string => {
        return (mockLocalstore[key] = <string>value);
      }
    );
    spyOn(localStorage, 'removeItem').and.callFake((key: string): void => {
      delete mockLocalstore[key];
    });
    localStorage.setItem('token', 'AAA');
    accountService.logout().subscribe(() => {
      expect(localStorage.getItem('token')).toBe(null);
    });
  });
});
