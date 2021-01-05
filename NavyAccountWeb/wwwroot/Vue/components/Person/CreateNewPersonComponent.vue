

<template>
    <!-- WRAPPER -->

    <div class="col-md-12">

        <div v-if="errors" class="alert alert-danger alert-dismissible" role="alert">
            <div class="alert-message">
                {{ [errors] }}
            </div>
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">?</span>
            </button>
        </div>

        <div class="col-12">
            <form id="smartwizard-validation" class="wizard wizard-primary sw-main sw-theme-default" @submit="postPersonelAndBeneficiary" method="post" action="javascript:void(0)" novalidate="novalidate">
                <ul class="nav nav-tabs step-anchor">
                    <li class="nav-item active"><a href="#validation-step-1" class="nav-link">Personal Details</a></li>
                    <li class="nav-item"><a href="#validation-step-2" class="nav-link">Beneficiary</a></li>
                    
                </ul>

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
                                <input class="form-control" name="Phone1" v-model="postBody.person.Phone1" />
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
                      
                        
                    </div>
                   
                    <!-- <div id="validation-step-2" class="tab-pane step-content">
                        <h2>Beneficiary 1</h2>
                        <input type="hidden" v-model="postBody.bene.id" />
                        <div class="form-row">
                            <div class="col-sm-4">
                                <label>FirstName</label>
                                <input class="form-control" name="FirstName" v-model="postBody.bene.FirstName" placeholder="" />

                            </div>
                            <div class="col-sm-4">
                                <label>LastName</label>
                                <input class="form-control" name="LastName" v-model="postBody.bene.LastName" placeholder="" />

                            </div>
                            <div class="col-sm-4">
                                <label>Relationship</label>
                                <select class="form-control" v-model="postBody.bene.RelationshipId" name="rank" required>
                                    <option v-for="ge in relationship" v-bind:value="ge.value" v-bind:key="ge.value"> {{ ge.text }} </option>
                                </select>
                            </div>
                        </div>
                        <div class="form-row">


                            <div class="col-sm-4">
                                <label>Mobile Number</label>
                                <input class="form-control" name="FirstName" v-model="postBody.bene.MobileNumber" placeholder="" />

                            </div>

                            <div class="col-sm-4">
                                <label>Email</label>
                                <input class="form-control" name="EmailAddress" v-model="postBody.bene.EmailAddress" placeholder="" />

                            </div>
                            <div class="col-sm-4">
                                <label>Date Of Birth</label>
                                <vuejsDatepicker v-model="postBody.bene.dateofbirth" input-class="form-control" name="dateofbbene"></vuejsDatepicker>

                            </div>
                        </div>

                        <div class="form-row">
                            <div class="col-sm-4">
                                <label>Proportion</label>
                                <input class="form-control" name="Proportion" v-model="postBody.bene.Proportion" placeholder="" />
                            </div>

                            <div class="col-sm-4">
                                <label>Full address</label>
                                <input class="form-control" name="FullAddress" v-model="postBody.bene.FullAddress" placeholder="" />
                            </div>
                            <div class="col-sm-4">
                                <button v-if="addbene22" class="btn btn-submit btn-primary" v-on:click="addBeneTwo" type="button">Add More Beneficiary</button>
                             </div>
                            </div>

                            <div v-if="addbene2">
                                <h2>Beneficiary 2</h2>
                                <input type="hidden" v-model="postBody.bene2.id" />
                                <div class="form-row">
                                    <div class="col-sm-4">
                                        <label>FirstName</label>
                                        <input class="form-control" name="FirstName" v-model="postBody.bene2.FirstName" placeholder="" />

                                    </div>
                                    <div class="col-sm-4">
                                        <label>LastName</label>
                                        <input class="form-control" name="LastName" v-model="postBody.bene2.LastName" placeholder="" />

                                    </div>
                                    <div class="col-sm-4">
                                        <label>Relationship</label>
                                        <select class="form-control" v-model="postBody.bene2.RelationshipId" name="rank" required>
                                            <option v-for="ge in relationship" v-bind:value="ge.value" v-bind:key="ge.value"> {{ ge.text }} </option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-row">


                                    <div class="col-sm-4">
                                        <label>Mobile Number</label>
                                        <input class="form-control" name="FirstName" v-model="postBody.bene2.MobileNumber" placeholder="" />

                                    </div>

                                    <div class="col-sm-4">
                                        <label>Email</label>
                                        <input class="form-control" name="EmailAddress" v-model="postBody.bene2.EmailAddress" placeholder="" />

                                    </div>
                                    <div class="col-sm-4">
                                        <label>Date Of Birth</label>
                                        <vuejsDatepicker v-model="postBody.bene2.dateofbirth" input-class="form-control" name="bene2dateofbirth"></vuejsDatepicker>
                                    </div>
                                </div>

                                <div class="form-row">
                                    <div class="col-sm-4">
                                        <label>Proportion</label>
                                        <input class="form-control" name="Proportion" v-model="postBody.bene2.Proportion" placeholder="" />
                                    </div>

                                    <div class="col-sm-4">
                                        <label>Full address</label>
                                        <input class="form-control" name="FullAddress" v-model="postBody.bene2.FullAddress" placeholder="" />
                                    </div>
                                </div><br />
                                <button v-if="addbene33" class="btn btn-submit btn-primary" v-on:click="addBeneThree" type="button">Add More Beneficiary</button>
                            </div>

                            <div v-if="addbene3">
                                <h2>Beneficiary 3</h2>
                                <input type="hidden" v-model="postBody.bene3.id" />
                                <div class="form-row">
                                    <div class="col-sm-4">
                                        <label>FirstName</label>
                                        <input class="form-control" name="FirstName" v-model="postBody.bene3.FirstName" placeholder="" />

                                    </div>
                                    <div class="col-sm-4">
                                        <label>LastName</label>
                                        <input class="form-control" name="LastName" v-model="postBody.bene3.LastName" placeholder="" />

                                    </div>
                                    <div class="col-sm-4">
                                        <label>Relationship</label>
                                        <select class="form-control" v-model="postBody.bene3.RelationshipId" name="rank" required>
                                            <option v-for="ge in relationship" v-bind:value="ge.value" v-bind:key="ge.value"> {{ ge.text }} </option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-row">


                                    <div class="col-sm-4">
                                        <label>Mobile Number</label>
                                        <input class="form-control" name="FirstName" v-model="postBody.bene3.MobileNumber" placeholder="" />

                                    </div>

                                    <div class="col-sm-4">
                                        <label>Email</label>
                                        <input class="form-control" name="EmailAddress" v-model="postBody.bene3.EmailAddress" placeholder="" />

                                    </div>
                                    <div class="col-sm-4">
                                        <label>Date Of Birth</label>
                                        <vuejsDatepicker v-model="postBody.bene3.dateofbirth" input-class="form-control" name="bene3dateofbirth"></vuejsDatepicker>
                                    </div>
                                </div>

                                <div class="form-row">
                                    <div class="col-sm-4">
                                        <label>Proportion</label>
                                        <input class="form-control" name="Proportion" v-model="postBody.bene3.Proportion" placeholder="" />
                                    </div>

                                    <div class="col-sm-4">
                                        <label>Full address</label>
                                        <input class="form-control" name="FullAddress" v-model="postBody.bene3.FullAddress" placeholder="" />
                                    </div>
                                </div>

                            </div>
                        </div> -->


                </div>
            </form>
        </div>








    </div>

    <!-- END WRAPPER -->
</template>


<script>
    import vuejsDatepicker from 'vuejs-datepicker';
    export default {
        props:['persontoeditid'],
        components: {
            vuejsDatepicker
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
                        
                    },
                    // bene: {
                    //     FirstName: '',
                    //     LastName: '',
                    //     RelationshipId: '',
                    //     MobileNumber: '',
                    //     EmailAddress: '',
                    //     Proportion: '',
                    //     dateofbirth: ''
                    // },
                    // bene2: {
                    //     FirstName: '',
                    //     LastName: '',
                    //     RelationshipId: '',
                    //     MobileNumber: '',
                    //     EmailAddress: '',
                    //     Proportion: '',
                    //     dateofbirth: ''
                    // },
                    // bene3: {
                    //     FirstName: '',
                    //     LastName: '',
                    //     RelationshipId: '',
                    //     MobileNumber: '',
                    //     EmailAddress: '',
                    //     Proportion: '',
                    //     dateofbirth: ''
                    // }
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

        methods: {
            // addBeneTwo() {
            //     this.addbene2 = true;
            //     this.addbene22 = false;
            // },
            // addBeneThree() {
            //     this.addbene3 = true;
            //     this.addbene33 = false;
            // },
            postPersonelAndBeneficiary() {
                this.addOrEdit(this.persontoeditid)
            },
            addOrEdit(actionToPerform) {
               
                let uri = actionToPerform == 0 ? `/api/PersonAPI/createPerson` : `/api/PersonAPI/updatePerson`;
                let message = actionToPerform == 0 ? `Created` : `Updated`;
                alert(this.postBody.person.PersonID);
                //alert(this.postBody.person);
              
                axios.post(uri,this.postBody)
                    .then(response => {
                        this.responseMessage = response.data.responseDescription; this.canProcess = true;
                        if (response.data.responseCode == '200') {
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
                                // bene: {
                                //     FirstName: '',
                                //     LastName: '',
                                //     RelationshipId: '',
                                //     MobileNumber: '',
                                //     EmailAddress: '',
                                //     Proportion: '',
                                //     dateofbirth: '',FullAddress:'',
                                //     Id:0
                                // },
                                // bene2: {
                                //     FirstName: '',
                                //     LastName: '',
                                //     RelationshipId: '',
                                //     MobileNumber: '',
                                //     EmailAddress: '',
                                //     Proportion: '',
                                //     dateofbirth: '',FullAddress:'',
                                //     Id:0
                                // },
                                // bene3: {
                                //     FirstName: '',
                                //     LastName: '',
                                //     RelationshipId: '',
                                //     MobileNumber: '',
                                //     EmailAddress: '',
                                //     Proportion: '',
                                //     dateofbirth: '',
                                //     FullAddress:'',
                                //     Id:0
                                // }
                            };
                            alert(`Successfully ${message}`);
                            
                        }
                        else {
                            alert('An error Occured, '+response.data.responseDescription)
                        }
                    })
                    .catch(e => {
                        console.log(e);
                        //this.errors.push(e)
                    });
            }

        },
        
        
        mounted() {
            this.objectToClear = this.postBody;
                
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
                          this.postBody.person.bank = response.data.bankid
                        this.postBody.person.Phone1 = response.data.phone1
                        this.postBody.person.email = response.data.email
                        this.postBody.person.accountno = response.data.accountno
                       
                    })
                   // [{"id":0,eNumber":null,"emailAddress":"hicad@hicad.com","placeOfWork":null,"nextofkinType":null,"isActive":true,"createdDate":"2020-03-07T13:55:47.4199588","createdBy":"hicad@hicad.com","modifiedDate":"2020-03-07T13:55:47.4201679","modifiedBy":"hicad@hicad.com","person":null},{"id":0,"personID":13,"firstName":"rewr","lastName":null,"relationshipId":"Mother","dateofbirth":"2020-03-23T12:55:00","fullAddress":"tetetetete","mobileNumber":"08064487564","homeNumber":null,"emailAddress":"hicad@hicad.com","placeOfWork":null,"nextofkinType":null,"isActive":true,"createdDate":"2020-03-07T13:55:47.4896898","createdBy":"hicad@hicad.com","modifiedDate":"2020-03-07T13:55:47.4896925","modifiedBy":"hicad@hicad.com","person":null},{"id":0,"personID":13,"firstName":"rewrbene3","lastName":null,"relationshipId":"Mother","dateofbirth":"2020-03-16T12:55:00","fullAddress":"tetetetete","mobileNumber":"08064487564","homeNumber":null,"emailAddress":"hicad@hicad.com","placeOfWork":null,"nextofkinType":null,"isActive":true,"createdDate":"2020-03-07T13:55:47.7151256","createdBy":"hicad@hicad.com","modifiedDate":"2020-03-07T13:55:47.7151273","modifiedBy":"hicad@hicad.com","person":null}]
                    //  axios
                    // .get(`/api/Beneficiary/getAllBeneficiaries/${this.persontoeditid}`)
                    // .then(response => {
                    //     this.postBody.bene.Id = response.data[0].id
                    //     this.postBody.bene.FirstName = response.data[0].firstName
                    //     this.postBody.bene.LastName = response.data[0].lastName
                    //     this.postBody.bene.RelationshipId = response.data[0].relationshipId
                    //     this.postBody.bene.dateofbirth = response.data[0].dateofbirth
                    //     this.postBody.bene.MobileNumber = response.data[0].mobileNumber
                    //     this.postBody.bene.FullAddress = response.data[0].fullAddress
                    //     this.postBody.bene.EmailAddress = response.data[0].emailAddress



                    //      this.postBody.bene2.Id = response.data[1].id
                    //     this.postBody.bene2.FirstName = response.data[1].firstName
                    //     this.postBody.bene2.LastName = response.data[1].lastName
                    //     this.postBody.bene2.RelationshipId = response.data[1].relationshipId
                    //     this.postBody.bene2.dateofbirth = response.data[1].dateofbirth
                    //     this.postBody.bene2.MobileNumber = response.data[1].mobileNumber
                    //     this.postBody.bene2.FullAddress = response.data[1].fullAddress
                    //     this.postBody.bene2.EmailAddress = response.data[1].emailAddress



                    //      this.postBody.bene3.Id = response.data[2].id
                    //     this.postBody.bene3.FirstName = response.data[2].firstName
                    //     this.postBody.bene3.LastName = response.data[2].lastName
                    //     this.postBody.bene3.RelationshipId = response.data[2].relationshipId
                    //     this.postBody.bene3.dateofbirth = response.data[2].dateofbirth
                    //     this.postBody.bene3.MobileNumber = response.data[2].mobileNumber
                    //     this.postBody.bene3.FullAddress = response.data[2].fullAddress
                    //     this.postBody.bene3.EmailAddress = response.data[2].emailAddress
                       
                    // })
            }

        }
    };
</script>
