<template>
    <div>
        <div v-if="errors" class="has-error" style="color:red"> {{ [errors] }}</div>
        <div v-if="responseMessage" class="has-error" style="color:green"> {{ responseMessage }}</div>
        <form @submit="checkForm"  method="post">
                <div class="sw-container tab-content">
                    <div id="validation-step-1" class="tab-pane step-content col-12" style="display: block;">
       <div class="form-row">
                            <div class="form-group col-md-4" hidden>
                                <input class="form-control" name="SVC_NO" v-model="postBody.person.PersonID" :disabled="persontoeditid > 0" placeholder="" required />
                            </div>
                            <div class="form-group col-md-4">
                                <label>Service No</label>
                                <input class="form-control" name="SVC_NO" v-model="postBody.person.SVC_NO" :disabled="persontoeditid > 0" placeholder="" required />
                            </div>
                            <div class="form-group col-md-4">
                                <label>Rank</label>
                                <select class="form-control" v-model="postBody.person.rank" name="rank" required>
                                    <option v-for="rank in rankList" v-bind:value="rank.id" v-bind:key="rank.id"> {{ rank.description }} </option>
                                </select>
                                </div>
                                <div class="col-md-4">
                                    <label>Date Of Birth</label>
                                    <vuejsDatepicker v-model="postBody.person.BirthDate" input-class="form-control" name="dateofbirth"></vuejsDatepicker>
                                </div>

                            </div>
                        
                        <div class="form-row">
                            <div class="form-group col-md-4">
                                <label>SurName</label>
                                <input class="form-control" name="LastName" v-model="postBody.person.LastName" placeholder="" required />
                            </div>
                            <div class="form-group col-md-4">
                                <label>FirstName</label>
                                <input class="form-control" name="FirstName" v-model="postBody.person.FirstName" placeholder="" required />

                            </div>

                            <div class="form-group col-md-4">
                                <label>MiddleName</label>
                                <input class="form-control" name="MiddleName" v-model="postBody.person.MiddleName" placeholder=""  />
                            </div>

                        </div>

                        <div class="form-row">
                            <div class="form-group col-md-4">
                                <label>Sex</label>
                                <select class="form-control" v-model="postBody.person.Sex" name="rank" required>
                                    <option v-for="ge in gender" v-bind:value="ge.value" v-bind:key="ge.value"> {{ ge.text }} </option>
                                </select>
                            </div>
                            <div class="form-group col-md-4">
                                <label>Address</label>
                                <textarea class="form-control" name="homeaddress" v-model="postBody.person.homeaddress" placeholder="" />
                            </div>
                            <div class="col-md-4">
                                <label>PhoneNumber</label>
                                <input class="form-control" name="GSMNumber" v-model="postBody.person.GSMNumber" />
                            </div>
                        </div>

                        <div class="form-row">
                            <div class="col-sm-4">
                                <label>Email</label>
                                <input class="form-control" name="email" v-model="postBody.person.email" />
                            </div>
                            <div class="col-sm-4">
                                <label>Bank Name</label>
                                <select class="form-control" v-model="postBody.person.bank" name="bank" required>
                                    <option v-for="bank in bankList" v-bind:value="bank.id" v-bind:key="bank.id"> {{ bank.bankname }} </option>
                                </select>
                            </div>
                            <div class="col-sm-4">
                                <label>Account Number</label>
                                <input class="form-control" name="accountno" v-model="postBody.person.accountno" required />
                            </div>

                        </div>
                        <div class="form-row">

                            <div class="col-md-4">
                                <label>DateJoined</label>
                                <vuejsDatepicker v-model="postBody.person.dateemployed" input-class="form-control" name="dateemployed"></vuejsDatepicker>
                            </div>

                             <div class="col-md-4">
                                <label>Date Left</label>
                                <vuejsDatepicker v-model="postBody.person.dateleft" input-class="form-control" name="dateleft"></vuejsDatepicker>
                            </div>

                        </div>
                      
                      
                        <div class="row">
                    <div class="col-12 ">
                        <div class="btn-group mr-2 sw-btn-group-extra"  role="group">
                            <button class="btn btn-submit btn-primary" v-on:click="checkForm"   type="submit">{{submitorUpdate}}</button>
                        </div>
                    </div>
                   </div>
                    </div>

                </div>
        </form>
    </div>
</template>

<script>
import Axios from 'axios';
import vuejsAutocomplete from 'vuejs-auto-complete'
import vuejsDatepicker from 'vuejs-datepicker'
export default {
    props:['persontoeditid'],
    components: {
            vuejsDatepicker,
            vuejsAutocomplete,
            Axios
        },
          data() {
            return {
                rankList: null,
                bankList: null,
                errors: null,
                addbene2: false,
                addbene22: true,
                addbene3: false,
                addbene33: true,
                submitorUpdate:'Submit',
                responseMessage: '',
                objectToClear: {},
                postBody: {
                    person:
                    {
                        SVC_NO: '',
                        rank: '',
                        FirstName: '',
                        MiddleName: '',
                        LastName: '',
                        Sex: '',
                        homeaddress: '',
                        BirthDate: '',
                        dateemployed: '',
                        dateleft:'',
                        Phone1: '',
                        email: '',
                        bank: '',
                        accountno: '',
                        PersonID : 0,
                        GSMNumber:''
                        
                    },

                },
                
                gender: [
                    { value: 'M', text: 'Male' },
                    { value: 'F', text: 'Female' }
                ],
                relationship: [
                    { value: 'Wife', text: 'Wife' },
                    { value: 'Son', text: 'Son' },
                    { value: 'Father', text: 'Father' },
                    { value: 'Mother', text: 'Mother' },
                    { value: 'Brother', text: 'Brother' },
                    { value: 'Sister', text: 'Sister' }
                  
                ]
            };
        },
    mounted(){
           axios
                .get('/api/Rank/getAllRanks')
                .then(response => (this.rankList = response.data));
            axios
                .get('/api/Bank/getAllBanks')
                .then(response => (this.bankList = response.data));
        
         if (this.persontoeditid != 0) {
                axios
                    .get(`/api/PersonAPI/getPersonByID/${this.persontoeditid}`)
                    .then(response => {
                        alert(response.data.rankid);
                        this.postBody.person.SVC_NO = response.data.svC_NO
                        this.postBody.person.PersonID = response.data.personID
                        this.postBody.person.rank = response.data.rankid
                        this.postBody.person.FirstName = response.data.firstName
                        this.postBody.person.MiddleName = response.data.middleName == null ? '' : response.data.middleName
                        this.postBody.person.LastName = response.data.lastName
                        this.postBody.person.Sex = response.data.sex
                        this.postBody.person.homeaddress = response.data.homeaddress
                        this.postBody.person.BirthDate = response.data.birthDate
                        this.postBody.person.dateemployed = response.data.dateemployed
                         this.postBody.person.dateleft = response.data.dateleft
                          this.postBody.person.bank = response.data.bankname
                        this.postBody.person.GSMNumber = response.data.gSMNumber
                        this.postBody.person.email = response.data.email
                        this.postBody.person.accountno = response.data.accountno
                       
                    })
                  
            }
    },
    methods:{
            
        checkForm: function (e) {
            if (this.postBody.svC_NO) {
                 e.preventDefault();
                 this.canProcess = false;
                 this.postData();
             }
             else{
   
             this.errors = [];
               this.errors.push('Code is Required');
             }
           },
        postData(){
            if(this.submitorUpdate=='Submit'){
            Axios.post(`/api/PersonAPI/createPerson`,this.postBody)
            .then(response=>{
                this.responseMessage=response.data.responseDescription;
                this.canProcess = true;
                if(response.data.responseCode=='200'){
                    this.postBody = {
                                person:
                                {
                                    SVC_NO: '',
                                    rank: '',
                                    FirstName: '',
                                    MiddleName: '',
                                    LastName: '',
                                    Sex: '',
                                    homeaddress: '',
                                    BirthDate: '',
                                    dateemployed: '',
                                    Phone1: '',
                                    email: '',
                                    bank: '',
                                    accountno: '',

                                },
                            };
                            alert(`Successfully ${message}`);
              
                }
            }).catch(e=>{
                this.errors.push(e);
            })
            }
            if(this.submitorUpdate=='update'){
                Axios.put(`/api/PersonAPI/updatePerson`,this.postBody)
                .then(response=>{
                this.responseMessage=response.data.responseDescription;
                if(response.data.responseCode=='200'){
                    this.submitorUpdate='Submit';
                     this.postBody = {
                                person:
                                {
                                    SVC_NO: '',
                                    rank: '',
                                    FirstName: '',
                                    MiddleName: '',
                                    LastName: '',
                                    Sex: '',
                                    homeaddress: '',
                                    BirthDate: '',
                                    dateemployed: '',
                                    Phone1: '',
                                    email: '',
                                    bank: '',
                                    accountno: '',

                                },
                            };
                            alert(`Successfully ${message}`);
                }
            }).catch(e=>{
                this.errors.push(e);
            })
            }
        }
       
    },
    computed:{

    }
}
</script>
